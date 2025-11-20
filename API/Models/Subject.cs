using API.Enums;

namespace API.Models;

public class Subject : BaseClass
{
  public string Code { get; set; } = string.Empty;
  public string Title { get; set; } = string.Empty;
  public short ECTS { get; set; }
  public bool IsObligatory { get; set; }
  public FieldOfStudy FieldOfStudy { get; set; }
  public IEnumerable<Professor>? Professors { get; set; }
}
