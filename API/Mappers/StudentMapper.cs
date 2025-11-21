using API.DTOs.StudentDtos;
using API.Models;

namespace API.Mappers;

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
      Email = entity.Email,
      Gender = entity.Gender,
      DateOfBirth = entity.DateOfBirth,
      Birthplace = entity.Birthplace,
      FieldOfStudy = entity.FieldOfStudy,
      Status = entity.Status,
      ContactInfo = entity.ContactInfo?.ToDto(),
      CreatedAt = entity.CreatedAt,
      UpdatedAt = entity.UpdatedAt
    };
  }

  public static Student ToEntity(this AddStudentDto dto)
  {
    return new Student()
    {
      PublicId = dto.PublicId,
      FirstName = dto.FirstName,
      LastName = dto.LastName,
      Gender = dto.Gender,
      DateOfBirth = dto.DateOfBirth,
      Birthplace = dto.Birthplace,
      FieldOfStudy = dto.FieldOfStudy,
      Status = dto.Status,
      ContactInfo = dto.ContactInfo?.ToEntity(),
    };
  }
}
