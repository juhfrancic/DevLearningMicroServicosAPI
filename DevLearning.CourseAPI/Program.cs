using DevLearning.CourseAPI.Repositories;
using DevLearning.CourseAPI.Services;
using Infrastructure.Data.Mongo.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddHttpClient("Author", client =>
{
    client.BaseAddress = new Uri("https://localhost:7001");
});

builder.Services.AddHttpClient("Category", client =>
{
    client.BaseAddress = new Uri("https://localhost:7021");
});

builder.Services.AddHttpClient("Student", client =>
{
    client.BaseAddress = new Uri("https://localhost:7263");
});

builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddScoped<CourseRepository>();
builder.Services.AddScoped<CourseService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
