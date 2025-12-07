using DevLearning.CategoryAPI.Data;
using DevLearning.CategoryAPI.Repositories;
using DevLearning.CategoryAPI.Repositories.Interfaces;
using DevLearning.CategoryAPI.Services;
using DevLearning.CategoryAPI.Services.Interfaces;
using DevLearning.CourseAPI.Repositories;
using DevLearning.CourseAPI.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<ConnectionDBCategory>();

builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();



var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
