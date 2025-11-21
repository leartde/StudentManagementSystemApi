using API.Enums;

namespace API.Models;

public class Student : BaseClass
{
  public string PublicId { get; set; } = string.Empty;
  public string FirstName { get; set; } = string.Empty;
  public string LastName { get; set; } = string.Empty;
  public string Email
  {
    get
    {
      if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName) || string.IsNullOrEmpty(PublicId))
        return string.Empty;
            
      return $"{char.ToLower(FirstName[0])}{char.ToLower(LastName[0])}{PublicId.AsSpan(4)}@ubt-uni.net";
    }
  }
  public Gender Gender { get; set; }
  public DateOnly DateOfBirth { get; set; }
  public string Birthplace { get; set; } = string.Empty;
  public FieldOfStudy FieldOfStudy { get; set; }
  public StudentStatus Status { get; set; }
  public ContactInfo? ContactInfo { get; set; }
  public int ContactInfoId { get; set; }
  public IEnumerable<Grade>? Grades { get; set; }
}
