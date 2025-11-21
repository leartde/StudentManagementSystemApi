using API.Enums;

namespace API.DTOs.SubjectDtos;

public class AddSubjectDto 
{
  public string Code { get; set; } = string.Empty;
  public string Title { get; set; } = string.Empty;
  public short ECTS { get; set; }
  public bool IsObligatory { get; set; }
  public FieldOfStudy FieldOfStudy { get; set; }
}
