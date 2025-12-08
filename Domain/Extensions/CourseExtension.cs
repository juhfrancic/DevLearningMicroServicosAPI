using Domain.Models;
using Domain.Models.DTOs.Course;

namespace Domain.Extensions;

public static class CourseExtension
{
    public static Course ToEntity(this CourseRequestDTO dto)
    {
        if (dto is null)
            return null;

        return new Course(
                 dto.Tag,
                 dto.Title,
                 dto.Summary,
                 dto.Url,
                 dto.Level,
                 dto.DurationInMinutes,
                 DateTime.UtcNow,
                 DateTime.UtcNow,
                 true,
                 false,
                 false,
                 dto.AuthorId,
                 dto.CategoryId,
                 dto.Tags
                 );
    }
    public static CourseResponseDTO ToDto(this Course course)
    {
        if (course is null)
            return null;

        return new CourseResponseDTO
        {
            CourseId = course.Id.ToString(),
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
