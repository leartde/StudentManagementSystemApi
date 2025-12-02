namespace API.Shared.Validators;

public static class CommonValidationRules
{
  public static bool ContainsOnlyLetters(string word, int? minLength = 2, int? maxLength = 35)
  {
    return !string.IsNullOrEmpty(word) && word.All(char.IsLetter) && word.Length >= minLength && word.Length <= maxLength;
  }
  
}
