using Domain.Models.DTOs.Course;

namespace Domain.Models.DTOs.Author
{
    public class AuthorWithCoursesDTO
    {
        public string AuthorName { get; set; }
        public List<CourseResponseDTO> Courses { get; set; }
    }
}
