namespace API.DTOs.ProfessorDtos;

public class AddProfessorDto
{
  public string FirstName { get; set; } = string.Empty;
  public string LastName { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  // public ContactInfo? ContactInfo { get; set; }
}
