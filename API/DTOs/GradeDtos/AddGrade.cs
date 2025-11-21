namespace API.DTOs.GradeDtos;

public class AddGrade 
{
  public int StudentId { get; set; }
  public int SubjectId { get; set; }
  public short Mark { get; set; }
}
