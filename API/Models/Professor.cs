namespace API.Models;

public class Professor : BaseClass
{
  public string FirstName { get; set; } = string.Empty;
  public string LastName { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public ContactInfo? ContactInfo { get; set; }
  public int ContactInfoId { get; set; }
  public IEnumerable<Subject>? Subjects { get; set; }
}
