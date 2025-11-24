using API.Enums;

namespace API.RequestFeatures;

public class SubjectParameters : RequestParameters
{
  public int? ProfessorId { get; set; }
  public FieldOfStudy? FieldOfStudy { get; set; }
}
