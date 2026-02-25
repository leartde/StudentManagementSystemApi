using API.Models;
using API.Shared.DTOs.StudentDtos;

namespace API.Shared.Mappers;

public static class StudentMapper
{
  public static ViewStudentDto ToDto(this Student entity)
  {
    return new ViewStudentDto
    {
      Id = entity.Id,
      PublicId = entity.PublicId,
      FirstName = entity.FirstName,
      LastName = entity.LastName,
      RegistrationYear = entity.RegistrationYear,
      Email = entity.Email,
      AverageGrade = entity.AverageGrade,
      Gender = entity.Gender,
      DateOfBirth = entity.DateOfBirth,
      Birthplace = entity.Birthplace,
      FieldOfStudy = entity.FieldOfStudy.ToString(),
      Status = entity.Status.ToString(),
      ContactInfo = entity.ContactInfo?.ToDto(),
      CreatedAt = entity.CreatedAt,
      UpdatedAt = entity.UpdatedAt,
    };
  }

  public static Student ToEntity(this AddStudentDto dto)
  {
    return new Student
    {
      FirstName = dto.FirstName,
      LastName = dto.LastName,
      RegistrationYear = dto.RegistrationYear,
      Gender = dto.Gender,
      DateOfBirth = dto.DateOfBirth,
      Birthplace = dto.Birthplace,
      FieldOfStudy = dto.FieldOfStudy,
      Status = dto.Status,
      ContactInfo = dto.ContactInfo?.ToEntity()
    };
  }

  public static void ToEntity(this UpdateStudentDto dto, Student entity)
  {
    entity.FirstName = dto.FirstName;
    entity.LastName = dto.LastName;
    entity.Gender = dto.Gender;
    entity.DateOfBirth = dto.DateOfBirth;
    entity.Birthplace = dto.Birthplace;
    entity.FieldOfStudy = dto.FieldOfStudy;
    entity.Status = dto.Status;
    entity.UpdatedAt = DateTime.UtcNow;
    if (dto.ContactInfo is null) return;
    if (entity.ContactInfo is not null)
    {
      dto.ContactInfo.ToEntity(entity.ContactInfo);
    }
    else
    {
      entity.ContactInfo = new ContactInfo();
      dto.ContactInfo.ToEntity(entity.ContactInfo);
    }
  }
}
