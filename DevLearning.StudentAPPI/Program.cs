using DevLearning.StudentAPI.Repositories;
using DevLearning.StudentAPI.Services;
using Infrastructure.Data.Mongo.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<MongoDbContext>();

builder.Services.AddHttpClient("Course", course =>
{
    course.BaseAddress = new Uri("https://localhost:7268");
});

builder.Services.AddScoped<StudentRepository>();
builder.Services.AddScoped<StudentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
