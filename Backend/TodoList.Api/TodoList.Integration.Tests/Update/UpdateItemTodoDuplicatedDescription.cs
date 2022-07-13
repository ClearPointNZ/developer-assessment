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

namespace TodoList.Integration.Tests.Update;

public class UpdateItemTodoDuplicatedDescription : IClassFixture<CustomWebApplicationFactory<Startup>>, IAsyncLifetime

{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Startup> _factory;
    private HttpResponseMessage _response;
    private readonly TodoContext _dbContext;
    private TodoItem? changedItem;


    public UpdateItemTodoDuplicatedDescription(CustomWebApplicationFactory<Startup> factory)
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
        _response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        changedItem.Description.Should().Be("desctiption");
    }

    public async Task InitializeAsync()
    {
        var todoItem = new TodoItem() {Description = "desctiption"};
        var todoItemDuplicated = new TodoItem() {Description = "duplicated"};
        await _dbContext.TodoItems.AddRangeAsync(todoItem, todoItemDuplicated);
        await _dbContext.SaveChangesAsync(CancellationToken.None);
        
        var createTodoRequest = new CreateTodoItemRequest()
        {
            Description = "duplicated",
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