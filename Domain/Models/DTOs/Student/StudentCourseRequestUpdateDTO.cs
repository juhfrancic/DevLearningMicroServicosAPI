using Domain.Models.Enums.StudentCourse;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.DTOs.Student
{
    public class StudentCourseRequestUpdateDTO
    {
        [Range(0, 100)]
        public byte Progress { get; set; }

        public FavoriteType Favorite { get; set; }
    }
}
