namespace API.Models;

public abstract class BaseClass
{
  public int Id { get; set; }
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public DateTime? UpdatedAt { get; set; }
  public bool IsDeleted { get; set; }
  public DateTime? DeletedAt { get; set; }
}
