using API.Enums;

namespace API.Models;

public class Student : BaseClass
{
  public string PublicId { get; set; } = string.Empty;
  public string FirstName { get; set; } = string.Empty;
  public string LastName { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public char Gender { get; set; }
  public DateOnly DateOfBirth { get; set; }
  public string Birthplace { get; set; } = string.Empty;
  public FieldOfStudy FieldOfStudy { get; set; }
  public StudentStatus Status { get; set; }
  public ContactInfo? ContactInfo { get; set; }
  
}
