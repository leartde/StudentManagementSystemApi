using API.Enums;

namespace API.Models;

public class Grade
{
  public Student? Student { get; set; }
  public int StudentId { get; set; }
  public Subject? Subject { get; set; }
  public int SubjectId { get; set; }
  public byte Mark { get; set; }
  public GradeStatus GradeStatus { get; set; } = GradeStatus.Pending;
  public DateTime CreatedAt { get; set; } = DateTime.Now;
  public DateTime? UpdatedAt { get; set; }
  public bool IsDeleted { get; set; }
  public DateTime? DeletedAt { get; set; }
}
