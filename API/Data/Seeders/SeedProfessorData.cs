using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Data.Seeders;

public class SeedProfessorData : IEntityTypeConfiguration<Professor>
{
  private static readonly DateTime SeedCreatedAt = new(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);

  public void Configure(EntityTypeBuilder<Professor> builder)
  {
    builder.HasData(
      new Professor
      {
        Id = 1,
        FirstName = "Ardit",
        LastName = "Musa",
        Email = "ardit.musa@ubt-uni.net",
        ContactInfoId = 1,
        CreatedAt = SeedCreatedAt
      },
      new Professor
      {
        Id = 2,
        FirstName = "Sara",
        LastName = "Shala",
        Email = "sara.shala@ubt-uni.net",
        ContactInfoId = 2,
        CreatedAt = SeedCreatedAt
      });
  }
}
