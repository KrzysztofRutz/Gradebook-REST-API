using Gradebook.Infrastructure;
using Gradebook.Application;
using Gradebook.Presentation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddPresentation();

builder.Host.UseInfrastructure();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UsePresntation();
app.UseApllication();

app.Run();

public partial class Program { }