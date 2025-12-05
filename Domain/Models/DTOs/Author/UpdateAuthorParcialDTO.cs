using Domain.Models.Enums.Author;

namespace Domain.Models.DTOs.Author
{
    public class UpdateAuthorParcialDTO
    {
        public string? Name { get; set; }
        public string? Title { get; set; }
        public string? Image { get; set; }
        public string? Bio { get; set; }
        public string? Url { get; set; }
        public AuthorType? Type { get; set; }


    }
}
