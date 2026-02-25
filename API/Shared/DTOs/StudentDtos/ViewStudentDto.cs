using API.Enums;
using API.Shared.DTOs.ContactInfoDtos;

namespace API.Shared.DTOs.StudentDtos;

public class ViewStudentDto
{
  public int Id { get; set; }
  public string PublicId { get; set; } = string.Empty;
  public string FirstName { get; set; } = string.Empty;
  public string LastName { get; set; } = string.Empty;
  public int RegistrationYear { get; set; }
  public string Email { get; set; } = string.Empty;
  public double AverageGrade { get; set; }
  public Gender Gender { get; set; }
  public DateOnly DateOfBirth { get; set; }
  public string Birthplace { get; set; } = string.Empty;
  public string FieldOfStudy { get; set; } = string.Empty;
  public string Status { get; set; } = string.Empty;
  public ViewContactInfoDto? ContactInfo { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime? UpdatedAt { get; set; }
}
