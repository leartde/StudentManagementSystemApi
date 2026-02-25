using API.Shared.DTOs.StudentDtos;
using API.Shared.DTOs.SubjectDtos;

namespace API.Shared.DTOs.GradeDtos;

public class ViewGradeDto
{
  public ViewStudentDto? Student { get; set; }
  public ViewSubjectDto? Subject { get; set; }
  public byte Mark { get; set; }
  public string GradeStatus { get; set; } = string.Empty;
  public DateTime CreatedAt { get; set; }
  public DateTime? UpdatedAt { get; set; }
}
