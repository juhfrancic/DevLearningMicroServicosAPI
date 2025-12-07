using MongoDB.Bson;

namespace Domain.Models.DTOs.CareerItem
{
    public class CareerItemResponseDTO
    {
        public ObjectId CourseId { get; init; }
        public string Title { get; init; }        
        public string Description { get; init; }
        public byte Order { get; init; }
        public string CourseTitle { get; init; }
    }
}
