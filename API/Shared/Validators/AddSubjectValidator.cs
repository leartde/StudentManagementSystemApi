using API.Shared.DTOs.StudentDtos;
using API.Shared.DTOs.SubjectDtos;

namespace API.Shared.Validators;

public class AddSubjectValidator : IValidator<AddSubjectDto>
{
  public ValidationResult Validate(AddSubjectDto entity)
  {
    var validationResult = new ValidationResult();
    if (string.IsNullOrEmpty(entity.Code) || entity.Code.Length != 6)
    {
      validationResult.Errors.Add("code",
        "Subject code cannot be empty and must be a value of 6 characters.");
    }

    if (string.IsNullOrEmpty(entity.Title) || entity.Title.Length < 4 || entity.Title.Length > 35)
    {
      validationResult.Errors.Add("title",
        "Title cannot be empty and must be a value between 4 and 35 characters.");
    }

    if (entity.ECTS is < 1 or > 15)
    {
      validationResult.Errors.Add("ects",
        "ECTs value must be between 1 and 15");
    }

    if ((int)entity.FieldOfStudy is > 7 or < 1)
    {
      validationResult.Errors.Add("fieldOfStudy",
        "Field of study must be a value between 1 or 7.");
    }

    return validationResult;
  }
}
