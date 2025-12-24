using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Data.Seeders;

public class SeedProfessorData : IEntityTypeConfiguration<Professor>
{
  public static readonly List<Professor> Professors = new();
  public void Configure(EntityTypeBuilder<Professor> builder)
  {
    for (int i = 0; i < 40; i++)
    {
      Professors.Add(new Professor
      {
        Id = i + 1,
        FirstName = Faker.Name.FirstName(),
        LastName = Faker.Name.LastName(),
        Email = "professor@example.com",
        ContactInfoId = 1
      });
    }

    builder.HasData(Professors);
  }
}
