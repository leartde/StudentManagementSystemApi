using API.Enums;

namespace API.Shared.RequestFeatures;

public class SubjectParameters : RequestParameters
{
  public int? ProfessorId { get; set; }
  public FieldOfStudy? FieldOfStudy { get; set; }
}
