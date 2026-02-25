using API.Enums;
using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Data.Seeders;

public class SeedStudentData : IEntityTypeConfiguration<Student>
{
  private static readonly DateTime SeedCreatedAt = new(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);

  public void Configure(EntityTypeBuilder<Student> builder)
  {
    builder.HasData(
      new Student
      {
        Id = 1,
        PublicId = "252600001",
        FirstName = "Aron",
        LastName = "Krasniqi",
        RegistrationYear = 2025,
        Gender = Gender.Male,
        DateOfBirth = new DateOnly(2006, 1, 10),
        Birthplace = "Prishtina",
        FieldOfStudy = FieldOfStudy.ComputerScience,
        ContactInfoId = 1,
        CreatedAt = SeedCreatedAt
      },
      new Student
      {
        Id = 2,
        PublicId = "252600002",
        FirstName = "Elena",
        LastName = "Berisha",
        RegistrationYear = 2025,
        Gender = Gender.Female,
        DateOfBirth = new DateOnly(2006, 3, 22),
        Birthplace = "Prishtina",
        FieldOfStudy = FieldOfStudy.Law,
        ContactInfoId = 2,
        CreatedAt = SeedCreatedAt
      });
  }
}
