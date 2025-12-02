using API.Enums;

namespace API.Models;

public class Subject : BaseClass
{
  public string Code { get; set; } = string.Empty;
  public string Title { get; set; } = string.Empty;
  public byte ECTS { get; set; }
  public bool IsObligatory { get; set; }
  public FieldOfStudy FieldOfStudy { get; set; }
  public Professor? Professor { get; set; }
  public int ProfessorId { get; set; }
}
