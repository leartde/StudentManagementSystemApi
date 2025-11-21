using API.Enums;

namespace API.DTOs.SubjectDtos;

public class ViewSubjectDto
{
  public int Id { get; set; }
  public string Code { get; set; } = string.Empty;
  public string Title { get; set; } = string.Empty;
  public short ECTS { get; set; }
  public bool IsObligatory { get; set; }
  public FieldOfStudy FieldOfStudy { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
}
