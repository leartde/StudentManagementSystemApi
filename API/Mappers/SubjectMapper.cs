using API.DTOs.SubjectDtos;
using API.Models;

namespace API.Mappers;

public static class SubjectMapper
{
  public static ViewSubjectDto ToDto(this Subject entity)
  {
    return new ViewSubjectDto
    {
      Id = entity.Id,
      ProfessorId = entity.ProfessorId ,
      Code = entity.Code,
      Title = entity.Title,
      ECTS = entity.ECTS,
      IsObligatory = entity.IsObligatory,
      FieldOfStudy = entity.FieldOfStudy.ToString(),
      ProfessorName = entity.Professor?.FirstName + " " + entity.Professor?.LastName,
      CreatedAt = entity.CreatedAt,
      UpdatedAt = entity.UpdatedAt,
      DeletedAt = entity.DeletedAt
    };
  }

  public static Subject ToEntity(this AddSubjectDto dto)
  {
    return new Subject
    {
      ProfessorId = dto.ProfessorId,
      Code = dto.Code,
      Title = dto.Title,
      ECTS = dto.ECTS,
      IsObligatory = dto.IsObligatory,
      FieldOfStudy = dto.FieldOfStudy,
    };
  }

  public static void ToEntity(this UpdateSubjectDto dto, Subject entity)
  {
    entity.ProfessorId = dto.ProfessorId;
    entity.Code = dto.Code;
    entity.Title = dto.Title;
    entity.ECTS = dto.ECTS;
    entity.IsObligatory = dto.IsObligatory;
    entity.FieldOfStudy = dto.FieldOfStudy;
    entity.UpdatedAt = DateTime.UtcNow;
  }
}
