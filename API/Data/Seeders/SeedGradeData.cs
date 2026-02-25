using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Data.Seeders;

public class SeedGradeData : IEntityTypeConfiguration<Grade>
{
  private static readonly DateTime SeedCreatedAt = new(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);

  public void Configure(EntityTypeBuilder<Grade> builder)
  {
    builder.HasData(
      new Grade
      {
        StudentId = 1,
        SubjectId = 1,
        Mark = 10,
        CreatedAt = SeedCreatedAt
      },
      new Grade
      {
        StudentId = 2,
        SubjectId = 2,
        Mark = 9,
        CreatedAt = SeedCreatedAt
      });
  }
}
