using Domain.Models.Enums.StudentCourse;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models
{
    public class StudentCourse
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public ObjectId StudentCourseId { get; private set; }
        [BsonElement("courseId")]
        public ObjectId CourseId { get; private set; }
        [BsonElement("studentId")]
        public ObjectId StudentId { get; private set; }
        [BsonElement("progress")]
        public byte Progress { get; private set; }
        [BsonElement("favorite")]
        public FavoriteType Favorite { get; private set; }
        [BsonElement("startDate")]
        public DateTime StartDate { get; private set; }
        [BsonElement("lastUpdateDate")]
        public DateTime LastUpdateDate { get; private set; }

        public StudentCourse(ObjectId courseId, ObjectId studentId, byte progress, FavoriteType favorite)
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
