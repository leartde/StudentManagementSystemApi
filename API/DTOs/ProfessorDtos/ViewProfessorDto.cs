using API.DTOs.SubjectDtos;

namespace API.DTOs.ProfessorDtos;

public class ViewProfessorDto
{
  public int Id { get; set; }
  public string FirstName { get; set; } = string.Empty;
  public string LastName { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  // public ContactInfo? ContactInfo { get; set; }
  public IEnumerable<ViewSubjectDto>? Subjects { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
}
