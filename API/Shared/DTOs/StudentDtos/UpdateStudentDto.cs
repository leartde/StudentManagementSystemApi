using API.Enums;
using API.Shared.DTOs.ContactInfoDtos;

namespace API.Shared.DTOs.StudentDtos;

public class UpdateStudentDto
{
  public string FirstName { get; set; } = string.Empty;
  public string LastName { get; set; } = string.Empty;
  public Gender Gender { get; set; }
  public DateOnly DateOfBirth { get; set; }
  public string Birthplace { get; set; } = string.Empty;
  public FieldOfStudy FieldOfStudy { get; set; }
  public StudentStatus Status { get; set; }
  public UpdateContactInfoDto? ContactInfo { get; set; }
}
