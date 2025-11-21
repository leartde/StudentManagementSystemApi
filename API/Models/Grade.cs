using API.Enums;

namespace API.Models;

public class Grade : BaseClass
{
  public Student? Student { get; set; }
  public int StudentId { get; set; }
  public Subject? Subject { get; set; }
  public int SubjectId { get; set; } 
  public short Mark { get; set; }
  public GradeStatus GradeStatus { get; set; } = GradeStatus.Pending;
}
