using API.DTOs.StudentDtos;
using API.DTOs.SubjectDtos;
using API.Enums;

namespace API.DTOs.GradeDtos;

public class ViewGradeDto
{
  public int Id { get; set; }
  public ViewStudentDto? Student { get; set; }
  public ViewSubjectDto? Subject { get; set; }
  public byte Mark { get; set; }
  public string GradeStatus { get; set; } = string.Empty;
  public DateTime CreatedAt { get; set; }
  public DateTime? UpdatedAt { get; set; }
  public DateTime? DeletedAt { get; set; }
}
