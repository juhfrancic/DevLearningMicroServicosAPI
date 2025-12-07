using Domain.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Data.SQL.Contexts
{
    public class ConnectionDb
    {
        private readonly IMongoCollection<Student> _students;
        private readonly IMongoCollection<StudentCourse> _studentCourses;
        public ConnectionDb(IOptions<MongoDbSettings> mongoDbSettings)
        {
        

        MongoClient client = new MongoClient(mongoDbSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);

        _students = database.GetCollection<Student>("Students");
        _studentCourses = database.GetCollection<StudentCourse>("StudentCourses");
        }

    public IMongoCollection<Student> GetStudentCollection()
        {
            return _students;
        }

        public IMongoCollection<StudentCourse> GetStudentCourseCollection()
        {
            return _studentCourses;
        }
    }
}
