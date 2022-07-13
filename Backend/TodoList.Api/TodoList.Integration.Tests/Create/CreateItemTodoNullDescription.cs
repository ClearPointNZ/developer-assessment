using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using TodoList.Api;
using TodoList.Api.Models.Requests;
using Xunit;

namespace TodoList.Integration.Tests.Create;

public class CreateItemTodoNullDescription : IClassFixture<CustomWebApplicationFactory<Startup>>, IAsyncLifetime

{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Startup> _factory;
    private HttpResponseMessage _response;

    public CreateItemTodoNullDescription(CustomWebApplicationFactory<Startup> factory)
    {
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
        _factory = factory;
    }

    [Fact]
    public void Should_Be_Successful()
    {
        _response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    public async Task InitializeAsync()
    {
        var createTodoRequest = new CreateTodoItemRequest()
        {
            Description = null,
            IsCompleted = false
        };
        _response = await _client.PostAsJsonAsync("api/todoitems", createTodoRequest);
    }

    public async Task DisposeAsync()
    {
        _client.Dispose();
        _factory.Dispose();
    }
}