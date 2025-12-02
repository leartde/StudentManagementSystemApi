using API.Shared.DTOs.ContactInfoDtos;

namespace API.Shared.DTOs.ProfessorDtos;

public class AddProfessorDto
{
  public string FirstName { get; set; } = string.Empty;
  public string LastName { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public AddContactInfoDto? ContactInfo { get; set; }
}
