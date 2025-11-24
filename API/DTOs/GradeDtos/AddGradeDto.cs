namespace API.DTOs.GradeDtos;

public class AddGradeDto
{
  public int StudentId { get; set; }
  public int SubjectId { get; set; }
  public byte Mark { get; set; }
}
