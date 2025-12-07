using MongoDB.Bson;

namespace Domain.Models.DTOs.CareerItem
{
    public class CareerItemRequestDTO
    {
        public string CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte Order { get; set; }
    }
}
