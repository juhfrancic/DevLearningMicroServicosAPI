using DevLearning.API.DataBase;
using DevLearning.API.Repositories;
using DevLearning.API.Services;
using DevLearning.API.Repositories.Interfaces;
using DevLearning.API.Services.Interfaces;
using System.Data.Common;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<ConnectionDB>();
builder.Services.AddSingleton<CourseRepository>();
builder.Services.AddSingleton<CourseService>();

builder.Services.AddSingleton<CareerRepository>();
builder.Services.AddSingleton<CareerService>();
builder.Services.AddSingleton<CareerItemRepository>();
builder.Services.AddSingleton<CareerItemService>();



builder.Services.AddSingleton<ICategoryRepository, CategoryRepository>();
builder.Services.AddSingleton<ICategoryService, CategoryService>();


builder.Services.AddSingleton<AuthorRepository>();
builder.Services.AddSingleton<AuthorService>();

builder.Services.AddSingleton<StudentRepository>();
builder.Services.AddSingleton<StudentService>();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
