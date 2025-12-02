using API.Models;
using API.Shared.DTOs.ContactInfoDtos;

namespace API.Shared.Mappers;

public static class ContactInfoMapper
{
  public static ViewContactInfoDto ToDto(this ContactInfo entity)
  {
    return new ViewContactInfoDto
    {
      Id = entity.Id,
      Country = entity.Country,
      Residence = entity.Residence,
      Street = entity.Street,
      ZipCode = entity.ZipCode,
      PhoneNumber = entity.PhoneNumber,
      PrivateEmail = entity.PrivateEmail,
      Ethnicity = entity.Ethnicity,
      Nationality = entity.Nationality,
      CreatedAt = entity.CreatedAt,
      UpdatedAt = entity.UpdatedAt,
      DeletedAt = entity.DeletedAt
    };
  }

  public static ContactInfo ToEntity(this AddContactInfoDto dto)
  {
    return new ContactInfo
    {
      Country = dto.Country,
      Residence = dto.Residence,
      Street = dto.Street,
      ZipCode = dto.ZipCode,
      PhoneNumber = dto.PhoneNumber,
      PrivateEmail = dto.PrivateEmail,
      Ethnicity = dto.Ethnicity,
      Nationality = dto.Nationality
    };
  }

  public static void ToEntity(this UpdateContactInfoDto dto, ContactInfo entity)
  {
    entity.Country = dto.Country;
    entity.Residence = dto.Residence;
    entity.Street = dto.Street;
    entity.ZipCode = dto.ZipCode;
    entity.PhoneNumber = dto.PhoneNumber;
    entity.PrivateEmail = dto.PrivateEmail;
    entity.Ethnicity = dto.Ethnicity;
    entity.Nationality = dto.Nationality;
    entity.UpdatedAt = DateTime.UtcNow;
  }
}
