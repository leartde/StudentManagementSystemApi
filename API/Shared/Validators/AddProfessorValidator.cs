using API.Shared.DTOs.ProfessorDtos;
using static API.Shared.Validators.CommonValidationRules;

namespace API.Shared.Validators;

public class AddProfessorValidator : IValidator<AddProfessorDto>
{
  public ValidationResult Validate(AddProfessorDto entity)
  {
    var validationResult = new ValidationResult();
    if (!ContainsOnlyLetters(entity.FirstName))
    {
      validationResult.Errors.Add("First name",
        "First name cannot be empty and must be a value between 2 and 35 alphabetic characters");
    }

    if (!ContainsOnlyLetters(entity.LastName))
    {
      validationResult.Errors.Add("Last name",
        "cannot be empty and must be a value between 2 and 35 alphabetic characters");
    }

    if (string.IsNullOrEmpty(entity.Email))
    {
      validationResult.Errors.Add("PrivateEmail", "Email cannot be empty");
    }

    return validationResult;
  }
}
