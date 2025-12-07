using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models
{
    public class Student
    {
        public Student(string name, string email, string? document, string? phone, DateTime birthdate)
        { 
            Name = name;
            Email = email;
            Document = document;
            Phone = phone;
            BirthDate = birthdate;
            CreateDate = DateTime.Now;
        }

        public Student()
        {
            
        }

        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public ObjectId Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string? Document { get; private set; }
        public string? Phone { get; private set; }
        public DateTime? BirthDate { get; private set; }
        public DateTime CreateDate { get; private set; }
    }
}
