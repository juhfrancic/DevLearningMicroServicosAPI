using Domain.Models;
using Domain.Models.DTOs.Course;

namespace Domain.Extensions;

public static class CourseExtension
{
    public static CourseResponseDTO ToDto(this Course course)
    {
        if (course is null)
            return null;

        return new CourseResponseDTO
        {
            CourseId = course.Id,
            Tag = course.Tag,
            Title = course.Title,
            Summary = course.Summary,
            Url = course.Url,
            Level = course.Level,
            DurationInMinutes = course.DurationInMinutes,
            CreateDate = course.CreateDate,
            LastUpdateDate = course.LastUpdateDate,
            Active = course.Active,
            Free = course.Free,
            Featured = course.Featured,
            AuthorName = course.AuthorName,
            CategoryName = course.CategoryName,
            Tags = course.Tags
        };
    }
}
