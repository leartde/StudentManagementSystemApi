using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Data.Seeders;

public class SeedContactInfoData : IEntityTypeConfiguration<ContactInfo>
{
  private static readonly DateTime SeedCreatedAt = new(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);

  public void Configure(EntityTypeBuilder<ContactInfo> builder)
  {
    builder.HasData(
      new ContactInfo
      {
        Id = 1,
        Country = "Kosovo",
        Residence = "Prishtina",
        Street = "SomeStreet",
        ZipCode = "10000",
        PhoneNumber = "044111111",
        PrivateEmail = "student.one@example.com",
        Ethnicity = "Albanian",
        Nationality = "Kosovar",
        CreatedAt = SeedCreatedAt
      },
      new ContactInfo
      {
        Id = 2,
        Country = "Kosovo",
        Residence = "Prishtina",
        Street = "AnotherStreet",
        ZipCode = "10000",
        PhoneNumber = "044222222",
        PrivateEmail = "student.two@example.com",
        Ethnicity = "Albanian",
        Nationality = "Kosovar",
        CreatedAt = SeedCreatedAt
      });
  }
}
