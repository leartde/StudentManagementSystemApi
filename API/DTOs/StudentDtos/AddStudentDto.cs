using API.DTOs.ContactInfoDtos;
using API.Enums;

namespace API.DTOs.StudentDtos;

public class AddStudentDto
{
  public string PublicId { get; set; } = string.Empty;
  public string FirstName { get; set; } = string.Empty;
  public string LastName { get; set; } = string.Empty;
  public Gender Gender { get; set; }
  public DateOnly DateOfBirth { get; set; }
  public string Birthplace { get; set; } = string.Empty;
  public FieldOfStudy FieldOfStudy { get; set; }
  public StudentStatus Status { get; set; }
  public AddContactInfoDto? ContactInfo { get; set; }
}
