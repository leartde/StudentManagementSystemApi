using API.DTOs.GradeDtos;
using API.Models;

namespace API.Mappers;

public static class GradeMapper
{
  public static ViewGradeDto ToDto(this Grade entity)
  {
    return new ViewGradeDto
    {
      Id = entity.Id,
      Student = entity.Student?.ToDto(),
      Subject = entity.Subject?.ToDto(),
      Mark = entity.Mark,
      GradeStatus = entity.GradeStatus.ToString(),
      CreatedAt = entity.CreatedAt,
      UpdatedAt = entity.UpdatedAt,
      DeletedAt = entity.DeletedAt
    };
  }

  public static Grade ToEntity(this AddGradeDto dto)
  {
    return new Grade
    {
      StudentId = dto.StudentId,
      SubjectId = dto.SubjectId,
      Mark = dto.Mark
    };
  }

  public static void ToEntity(this UpdateGradeDto dto, Grade entity)
  {
    entity.Mark = dto.Mark;
    entity.UpdatedAt = DateTime.UtcNow;
  }
}
