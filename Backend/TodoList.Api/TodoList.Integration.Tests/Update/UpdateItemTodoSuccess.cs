using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TodoList.Api;
using TodoList.Api.Models.Requests;
using TodoList.Data;
using TodoList.Data.Models;
using Xunit;

namespace TodoList.Integration.Tests.Create;

public class UpdateItemTodoSuccess : IClassFixture<CustomWebApplicationFactory<Startup>>, IAsyncLifetime

{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Startup> _factory;
    private HttpResponseMessage _response;
    private readonly TodoContext _dbContext;
    private TodoItem? changedItem;

    public UpdateItemTodoSuccess(CustomWebApplicationFactory<Startup> factory)
    {
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
        _factory = factory;
        var scope = _factory.Services.CreateScope();
        _dbContext = scope.ServiceProvider.GetRequiredService<TodoContext>();
    }

    [Fact]
    public void Should_Be_Successful()
    {
        _response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        changedItem.Description.Should().Be("This is a description");
    }

    public async Task InitializeAsync()
    {
        var todoItem = new TodoItem() {Description = "desctiption"};
        _dbContext.TodoItems.Add(todoItem);
        await _dbContext.SaveChangesAsync(CancellationToken.None);
        
        var createTodoRequest = new CreateTodoItemRequest()
        {
            Description = "This is a description",
            IsCompleted = false
        };
        _response = await _client.PutAsJsonAsync($"api/todoitems/{todoItem.Id}", createTodoRequest);
        changedItem = await _dbContext.TodoItems.AsNoTracking().Where(x => x.Id == todoItem.Id).SingleOrDefaultAsync(CancellationToken.None);
    }

    public async Task DisposeAsync()
    {
        await _dbContext.DisposeAsync();
        _client.Dispose();
        _factory.Dispose();
    }
}