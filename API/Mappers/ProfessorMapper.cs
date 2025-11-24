using API.DTOs.ProfessorDtos;
using API.Models;

namespace API.Mappers;

public static class ProfessorMapper
{
  public static ViewProfessorDto ToDto(this Professor entity)
  {
    return new ViewProfessorDto
    {
      Id = entity.Id,
      FirstName = entity.FirstName,
      LastName = entity.FirstName,
      Email = entity.Email,
      ContactInfo = entity.ContactInfo?.ToDto(),
      Subjects = entity.Subjects?.Select(s => s.ToDto()),
      CreatedAt = entity.CreatedAt,
      UpdatedAt = entity.UpdatedAt,
      DeletedAt = entity.DeletedAt
    };
  }

  public static Professor ToEntity(this AddProfessorDto dto)
  {
    return new Professor
    {
      FirstName = dto.FirstName,
      LastName = dto.LastName,
      Email = dto.Email,
      ContactInfo = dto.ContactInfo?.ToEntity()
    };
  }

  public static void ToEntity(this UpdateProfessorDto dto, Professor entity)
  {
    entity.FirstName = dto.FirstName;
    entity.LastName = dto.LastName;
    entity.Email = dto.Email;
    entity.UpdatedAt = DateTime.UtcNow;
    if (dto.ContactInfo == null) return;
    entity.ContactInfo = new ContactInfo();
    dto.ContactInfo.ToEntity(entity.ContactInfo);  }
}
