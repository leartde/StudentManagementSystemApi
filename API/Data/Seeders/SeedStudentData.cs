using API.Enums;
using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Data.Seeders;

public class SeedStudentData : IEntityTypeConfiguration<Student>
{
  public static readonly List<Student> Students = new();

  public void Configure(EntityTypeBuilder<Student> builder)
  {
    for (int i = 0; i < 100; i++)
    {
      Students.Add(new Student
      {
        Id = i + 1,
        PublicId = GeneratePublicId(2025),
        FirstName = i % 2 == 0 ? Faker.Name.MaleFirstName() : Faker.Name.FemaleFirstName(),
        LastName = Faker.Name.LastName(),
        RegistrationYear = 2025,
        Gender = i % 2 == 0 ? Gender.Male : Gender.Female,
        DateOfBirth = new DateOnly(2007, 01, 01),
        Birthplace = "Prishtina",
        FieldOfStudy = (FieldOfStudy)(i % 7 + 1),
        ContactInfoId = 1
      });
    }

    builder.HasData(Students);
  }

  private string GeneratePublicId(int registrationYear)
  {
    string publicId;
    bool isUnique;
    var random = new Random();
    do
    {
      var yearPart = (registrationYear % 100).ToString("00");
      var nextYearPart = ((registrationYear + 1) % 100).ToString("00");
      var randomPart = random.Next(0, 100000).ToString("00000");
      publicId = $"{yearPart}{nextYearPart}{randomPart}";
      isUnique = Students.Any(s => s.PublicId == publicId);
    } while (!isUnique);

    return publicId;
  }
}
