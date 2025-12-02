namespace API.Shared.Validators;

public interface IValidator<T>
{
  public ValidationResult Validate(T entity);
}
