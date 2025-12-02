using API.Enums;

namespace API.Models;

public class Result<T>
{
  private Result(bool success, T value,
    Dictionary<string, string>? errors = null,
    string? errorMessage = null,
    ErrorType? errorType = null)
  {
    Success = success;
    Value = value;
    Errors = errors;
    ErrorMessage = errorMessage;
    ErrorType = errorType.ToString();
  }

  public bool Success { get; }
  public T Value { get; }
  public Dictionary<string, string>? Errors { get; }
  public string? ErrorMessage { get; }
  public string? ErrorType { get; }

  public static Result<T> Ok(T value)
  {
    return new Result<T>(true, value);
  }

  public static Result<T> ValidationFail(Dictionary<string, string> errors)
  {
    return new Result<T>(false, default!, errors, null, Enums.ErrorType.Validation);
  }

  public static Result<T> Fail(string errorMessage, ErrorType errorType)
  {
    return new Result<T>(false, default!, null, errorMessage, errorType);
  }

  public static Result<T> NotFound(string message = "Resource not found")
  {
    return Fail(message, Enums.ErrorType.NotFound);
  }

  public static Result<T> Unauthorized(string message = "Unauthorized")
  {
    return Fail(message, Enums.ErrorType.Unauthorized);
  }

  public static Result<T> Conflict(string message = "Conflict")
  {
    return Fail(message, Enums.ErrorType.Conflict);
  }
}
