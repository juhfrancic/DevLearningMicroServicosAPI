using DevLearning.CategoryAPI.Repositories;
using DevLearning.CategoryAPI.Repositories.Interfaces;
using DevLearning.CategoryAPI.Services;
using DevLearning.CategoryAPI.Services.Interfaces;
using Infrastructure.Data.SQL.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<ConnectionDBCategory>();

builder.Services.AddScoped<CourseRepository>();
builder.Services.AddScoped<CategoryService>();

builder.Services.AddHttpClient("courseClient", client => client.BaseAddress = new Uri("https://localhost:7268"));


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
