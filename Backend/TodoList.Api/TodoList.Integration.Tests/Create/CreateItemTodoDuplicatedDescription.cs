using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using TodoList.Api;
using TodoList.Api.Models.Requests;
using TodoList.Data;
using TodoList.Data.Models;
using Xunit;

namespace TodoList.Integration.Tests.Create;

public class CreateItemTodoDuplicatedDescription : IClassFixture<CustomWebApplicationFactory<Startup>>, IAsyncLifetime

{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Startup> _factory;
    private HttpResponseMessage _response;
    private readonly TodoContext _dbContext;

    public CreateItemTodoDuplicatedDescription(CustomWebApplicationFactory<Startup> factory)
    {
        _factory = factory;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
        var scope = _factory.Services.CreateScope();
        _dbContext = scope.ServiceProvider.GetRequiredService<TodoContext>();
    }

    [Fact]
    public void Should_Be_Successful()
    {
        _response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    public async Task InitializeAsync()
    {
        _dbContext.TodoItems.Add(new TodoItem() {Description = "duplicated"});
        await _dbContext.SaveChangesAsync(CancellationToken.None);
        
        var createTodoRequest = new CreateTodoItemRequest()
        {
            Description = "duplicated",
            IsCompleted = false
        };
        _response = await _client.PostAsJsonAsync("api/todoitems", createTodoRequest);
    }

    public async Task DisposeAsync()
    {
        await _dbContext.DisposeAsync();
        _client.Dispose();
        await _factory.DisposeAsync();
    }
}