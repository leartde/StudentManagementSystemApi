using API.DTOs.ContactInfoDtos;
using API.Enums;

namespace API.DTOs.StudentDtos;

public class ViewStudentDto
{
  public int Id { get; set; }
  public string PublicId { get; set; } = string.Empty;
  public string FirstName { get; set; } = string.Empty;
  public string LastName { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public Gender Gender { get; set; }
  public DateOnly DateOfBirth { get; set; }
  public string Birthplace { get; set; } = string.Empty;
  public FieldOfStudy FieldOfStudy { get; set; }
  public StudentStatus Status { get; set; }
  public ViewContactInfoDto? ContactInfo { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
}
