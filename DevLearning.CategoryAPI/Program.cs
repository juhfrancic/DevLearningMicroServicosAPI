using DevLearning.CategoryAPI.Data;
using DevLearning.CategoryAPI.Repositories;
using DevLearning.CategoryAPI.Repositories.Interfaces;
using DevLearning.CategoryAPI.Services;
using DevLearning.CourseAPI.Repositories;
using Microsoft.AspNetCore.WebSockets;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<ConnectionDBCategory>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<CourseRepository>();
builder.Services.AddScoped<CategoryService>();



var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
