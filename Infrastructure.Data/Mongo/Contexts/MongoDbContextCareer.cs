using Domain.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Infrastructure.Data.Mongo.Contexts;

public class MongoDbContextCareer
{
    private readonly IMongoDatabase _database;

    public MongoDbContextCareer(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MongoDb");
        var databaseName = configuration["DatabaseName"];

        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }

    public IMongoCollection<Career> Careers
        => _database.GetCollection<Career>("Career");

    public IMongoCollection<CareerItem> CareerItems
        => _database.GetCollection<CareerItem>("CareerItem");

}
