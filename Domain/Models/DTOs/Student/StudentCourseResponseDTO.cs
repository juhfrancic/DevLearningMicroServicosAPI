using Domain.Models.DTOs.Course;
using Domain.Models.Enums.StudentCourse;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.DTOs.Student
{
    public class StudentCourseResponseDTO
    {
        [Range(0, 100)]
        public byte? Progress { get; init; } = 0;
        public FavoriteType Favorite { get; set; }
        public DateTime? StartDate { get; init; } = DateTime.Now;
        public DateTime? LastUpdateDate { get; set; }
        public StudentResponseDTO Student { get; set; }
        public CourseStudentDTO Course { get; set; }
    }
}
