using DeveloperAssessment.Modules.Todos.Api;
using DeveloperAssessment.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTodosModule();
builder.Services.AddSharedFramework(builder.Configuration);

var app = builder.Build();

app.UseSharedFramework();
app.UseTodosModule();

app.MapControllers();
app.MapGet("/", () => "DeveloperAssessment API");

app.Run();
