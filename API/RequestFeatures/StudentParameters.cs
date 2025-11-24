using API.Enums;

namespace API.RequestFeatures;

public class StudentParameters : RequestParameters
{
  public double? MinAverageGrade { get; set; }
  public FieldOfStudy? FieldOfStudy { get; set; }
}
