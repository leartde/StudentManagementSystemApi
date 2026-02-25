using API.Enums;
using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Data.Seeders;

public class SeedSubjectData : IEntityTypeConfiguration<Subject>
{
  private static readonly DateTime SeedCreatedAt = new(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);

  public void Configure(EntityTypeBuilder<Subject> builder)
  {
    builder.HasData(
      new Subject
      {
        Id = 1,
        Title = "Data Structures and Algorithms",
        Code = "DSA101",
        ECTS = 4,
        IsObligatory = true,
        FieldOfStudy = FieldOfStudy.ComputerScience,
        ProfessorId = 1,
        CreatedAt = SeedCreatedAt
      },
      new Subject
      {
        Id = 2,
        Title = "Criminal Law",
        Code = "LAW101",
        ECTS = 5,
        IsObligatory = true,
        FieldOfStudy = FieldOfStudy.Law,
        ProfessorId = 2,
        CreatedAt = SeedCreatedAt
      });
  }
}
