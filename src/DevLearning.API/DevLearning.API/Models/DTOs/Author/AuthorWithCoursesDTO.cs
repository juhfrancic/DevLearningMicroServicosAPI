namespace DevLearning.API.Models.DTOs.Author
{
    public class AuthorWithCoursesDTO
    {
        public string AuthorName { get; set; }
        public List<string> Courses { get; set; }
    }
}
