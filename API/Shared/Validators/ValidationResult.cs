using System.Text.Json;

namespace API.Shared.Validators;

public class ValidationResult
{
  public bool Success => Errors.Count == 0;
  public Dictionary<string, string> Errors { get; set; } = new();
}
