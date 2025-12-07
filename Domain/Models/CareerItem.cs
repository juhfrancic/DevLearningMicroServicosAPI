using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models
{
    public class CareerItem
    {
        public ObjectId CareerId { get; private set; }
        public ObjectId CourseId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public byte Order { get; private set; }

        public CareerItem(ObjectId careerId, ObjectId courseId, string title, string description, byte order)
        {
            CareerId = careerId;
            CourseId = courseId;
            Title = title;
            Description = description;
            Order = order;
        }
    }
}
