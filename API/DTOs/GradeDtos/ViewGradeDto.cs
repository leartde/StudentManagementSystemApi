using API.DTOs.StudentDtos;
using API.DTOs.SubjectDtos;
using API.Enums;

namespace API.DTOs.GradeDtos;

public class ViewGradeDto
{
  public int Id { get; set; }
  public ViewStudentDto? Student { get; set; }
  public ViewSubjectDto? Subject { get; set; }
  public short Mark { get; set; }
  public GradeStatus GradeStatus { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
}
