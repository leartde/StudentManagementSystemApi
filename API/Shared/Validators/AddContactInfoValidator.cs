using API.Shared.DTOs.ContactInfoDtos;
using static API.Shared.Validators.CommonValidationRules;

namespace API.Shared.Validators;

public class AddContactInfoValidator : IValidator<AddContactInfoDto>
{
  public ValidationResult Validate(AddContactInfoDto entity)
  {
    var validationResult = new ValidationResult();

    if (!ContainsOnlyLetters(entity.Country))
    {
      validationResult.Errors.Add("country",
        "Country cannot be empty and must be a value between 2 and 35 alphabetic characters.");
    }

    if (!ContainsOnlyLetters(entity.Residence))
    {
      validationResult.Errors.Add("residence",
        "Residence cannot be empty and must be a value between 2 and 35 alphabetic characters.");
    }

    if (string.IsNullOrEmpty(entity.Street) || entity.Street.Length < 4 || entity.Street.Length > 35)
    {
      validationResult.Errors.Add("street", "Street cannot be empty and must be a value between 4 and 35 characters.");
    }

    if (string.IsNullOrEmpty(entity.ZipCode) || entity.ZipCode.Length != 5 ||
        entity.ZipCode.Any(c => !char.IsNumber(c)))
    {
      validationResult.Errors.Add("zipCode", "Zip code cannot be empty and must be a value of 5 numerical characters.");
    }

    if (string.IsNullOrEmpty(entity.PhoneNumber) || entity.PhoneNumber.Length < 7 ||
        entity.PhoneNumber.Length > 20 || entity.PhoneNumber.Any(p => !char.IsNumber(p)))
      {
        validationResult.Errors.Add("phoneNumber",
          "Phone number cannot be empty and must be a value between 7 and 20 numerical characters.");
      }

      if (string.IsNullOrEmpty(entity.PrivateEmail))
      {
        validationResult.Errors.Add("privateEmail", "Private email cannot be empty");
      }

      if (!ContainsOnlyLetters(entity.Ethnicity))
      {
        validationResult.Errors.Add("ethnicity",
          "Ethnicity cannot be empty and must be a value between 2 and 35 alphabetic characters.");
      }

      if (!ContainsOnlyLetters(entity.Nationality))
      {
        validationResult.Errors.Add("nationality",
          "Nationality cannot be empty and must be a value between 2 and 35 alphabetic characters.");
      }

      return validationResult;
    }
  }
