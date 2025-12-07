using MongoDB.Bson;

namespace Domain.Models.DTOs.CareerItem
{
    public class CareerItemUpdateDTO
    {
        public ObjectId? CourseId { get; init; }
        public string? Title { get; init; }
        public string? Description { get; init; }
        public byte? Order { get; init; }
    }
}
