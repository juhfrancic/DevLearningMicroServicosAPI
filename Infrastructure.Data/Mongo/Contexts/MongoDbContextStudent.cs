using Domain.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Infrastructure.Data.Mongo.Contexts;

public class MongoDbContextStudent
{
    private readonly IMongoDatabase _database;

    public MongoDbContextStudent(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MongoDb");
        var databaseName = configuration["DatabaseName"];

        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }

    public IMongoCollection<Student> Students
       => _database.GetCollection<Student>("Students");

    public IMongoCollection<StudentCourse> StudentCourses
       => _database.GetCollection<StudentCourse>("StudentCourses");
}
