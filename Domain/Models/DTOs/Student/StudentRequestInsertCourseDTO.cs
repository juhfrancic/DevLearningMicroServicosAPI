using Domain.Models.Enums.StudentCourse;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.DTOs.Student
{
    public class StudentRequestInsertCourseDTO
    {
        [Range(0, 100)]
        public byte? Progress { get; init; } = 0;
        public FavoriteType Favorite { get; set; } = FavoriteType.No;
        public DateTime? StartDate { get; init; } = DateTime.Now;
    }
}
