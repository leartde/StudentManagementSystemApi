using API.Enums;

namespace API.Shared.DTOs.SubjectDtos;

public class UpdateSubjectDto
{
  public int ProfessorId { get; set; }
  public string Code { get; set; } = string.Empty;
  public string Title { get; set; } = string.Empty;
  public byte ECTS { get; set; }
  public bool IsObligatory { get; set; }
  public FieldOfStudy FieldOfStudy { get; set; }
}
