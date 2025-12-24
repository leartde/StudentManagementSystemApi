using API.Enums;
using API.Shared.DTOs.StudentDtos;
using static API.Shared.Validators.CommonValidationRules;

namespace API.Shared.Validators;

public class AddStudentValidator : IValidator<AddStudentDto>
{
  public ValidationResult Validate(AddStudentDto entity)
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
        "Last name cannot be empty and must be a value between 2 and 35 alphabetic characters");
    }

    if (entity.RegistrationYear > DateTime.UtcNow.Year || entity.RegistrationYear < 2010)
    {
      validationResult.Errors.Add("Registration year",
        $"Registration year must be a value between 2010 and {DateTime.UtcNow.Year}.");
    }

    if (entity.Gender is not (Gender.Male or Gender.Female))
    {
      validationResult.Errors.Add("Gender", "Gender must be a value between 1 (Male) or 2 (Female).");
    }

    if (!ContainsOnlyLetters(entity.Birthplace))
    {
      validationResult.Errors.Add("Birthplace",
        "Birthplace cannot be empty and must be a value between 2 and 35 alphabetic characters");
    }

    if ((int)entity.FieldOfStudy is > 7 or < 1)
    {
      validationResult.Errors.Add("Field of study",
        "Field of study must be a value between 1 or 7.");
    }
    
    if (entity.ContactInfo != null)
    {
      var contactInfoValidationResult = new AddContactInfoValidator().Validate(entity.ContactInfo);
      foreach (var error in contactInfoValidationResult.Errors)
      {
        validationResult.Errors.Add(error.Key, error.Value);
      }
    }
    else
    {
      validationResult.Errors.Add("Contact info",
        "Contact info cannot be null");
    }
    
    return validationResult;
  }
}
