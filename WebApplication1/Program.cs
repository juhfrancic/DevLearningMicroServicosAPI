using DevLearning.AuthorAPI.Repositories;
using DevLearning.AuthorAPI.Services;
using Infrastructure.Data.SQL.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<ConnectionDBAuthor>();

builder.Services.AddScoped<AuthorRepository>();
builder.Services.AddScoped<AuthorService>();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
