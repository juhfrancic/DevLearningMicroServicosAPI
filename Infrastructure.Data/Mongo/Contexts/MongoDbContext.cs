using Domain.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Infrastructure.Data.Mongo.Contexts;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MongoDb");
        var databaseName = configuration["DatabaseName"];

        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }

    public IMongoCollection<Course> Courses
        => _database.GetCollection<Course>("Courses");

    public IMongoCollection<Career> Careers
        => _database.GetCollection<Career>("Career");

    public IMongoCollection<CareerItem> CareerItems
        => _database.GetCollection<CareerItem>("CareerItem");

    public IMongoCollection<Student> Students
       => _database.GetCollection<Student>("Students");

    public IMongoCollection<StudentCourse> StudentCourses
       => _database.GetCollection<StudentCourse>("StudentCourses");
}
