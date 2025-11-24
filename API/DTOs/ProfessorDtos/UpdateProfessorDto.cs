using API.DTOs.ContactInfoDtos;

namespace API.DTOs.ProfessorDtos;

public class UpdateProfessorDto
{
  public string FirstName { get; set; } = string.Empty;
  public string LastName { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public UpdateContactInfoDto? ContactInfo { get; set; }
}
