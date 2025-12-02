namespace API.Shared.DTOs.SubjectDtos;

public class ViewSubjectDto
{
  public int Id { get; set; }
  public int ProfessorId { get; set; }
  public string Code { get; set; } = string.Empty;
  public string Title { get; set; } = string.Empty;
  public byte ECTS { get; set; }
  public bool IsObligatory { get; set; }
  public string FieldOfStudy { get; set; } = string.Empty;
  public string ProfessorName { get; set; } = string.Empty;
  public DateTime CreatedAt { get; set; }
  public DateTime? UpdatedAt { get; set; }
  public DateTime? DeletedAt { get; set; }
}
