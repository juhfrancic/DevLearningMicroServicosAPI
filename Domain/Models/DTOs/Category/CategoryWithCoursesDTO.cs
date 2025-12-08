using Domain.Models.DTOs.Course;

namespace Domain.Models.DTOs.Category
{
    public class CategoryWithCoursesDTO
    {
        public string CategoryTitle { get; set; }
        public List<CourseResponseDTO> Courses { get; set; }
    }
}
