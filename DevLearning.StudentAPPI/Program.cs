using DevLearning.StudentAPI.Repositories;
using DevLearning.StudentAPI.Repositories.Interfaces;
using DevLearning.StudentAPI.Services;
using DevLearning.StudentAPI.Services.Interfaces;
using Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<MongoDbSettings>();

builder.Services.AddHttpClient("Course", course =>
{
    course.BaseAddress = new Uri("https://localhost:7268");
});

builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
