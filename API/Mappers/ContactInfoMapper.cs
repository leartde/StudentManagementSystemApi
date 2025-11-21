using API.DTOs.ContactInfoDtos;
using API.Models;

namespace API.Mappers;

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
      Nationality = dto.Nationality,
    };
  }
}
