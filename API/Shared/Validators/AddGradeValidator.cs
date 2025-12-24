using API.Shared.DTOs.GradeDtos;

namespace API.Shared.Validators;

public class AddGradeValidator : IValidator<AddGradeDto>
{
  public ValidationResult Validate(AddGradeDto entity)
  {
    var validationResult = new ValidationResult();
    if (entity.Mark != 0 || (entity.Mark >= 5 && entity.Mark <= 10))
    {
      validationResult.Errors.Add("Mark",
        "Mark must either be a value of 0 or between 5 and 10");
    }

    return validationResult;
  }
}
