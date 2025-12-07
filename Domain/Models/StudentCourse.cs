using Domain.Models.Enums.StudentCourse;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class StudentCourse
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid StudentCourseId { get; private set; } = Guid.NewGuid();
        [BsonElement("courseId")]
        public Guid CourseId { get; private set; }
        [BsonElement("studentId")]
        public Guid StudentId { get; private set; }
        [BsonElement("progress")]
        public byte Progress { get; private set; }
        [BsonElement("favorite")]
        public FavoriteType Favorite { get; private set; }
        [BsonElement("startDate")]
        public DateTime StartDate { get; private set; }
        [BsonElement("lastUpdateDate")]
        public DateTime LastUpdateDate { get; private set; }

        public StudentCourse(Guid courseId, Guid studentId, byte progress, FavoriteType favorite)
        {
            CourseId = courseId;
            StudentId = studentId;
            Progress = progress;
            Favorite = favorite;
            StartDate = DateTime.Now;
            LastUpdateDate = DateTime.Now;
        }


    }
}
