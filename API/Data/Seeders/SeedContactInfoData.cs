using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Data.Seeders;

public class SeedContactInfoData : IEntityTypeConfiguration<ContactInfo>
{
  public void Configure(EntityTypeBuilder<ContactInfo> builder)
  {
    var contactInfo = new ContactInfo()
    {
      Id = 1,
      Country = "Kosovo",
      Residence = "Prishtina",
      Street = "SomeStreet",
      ZipCode = "10000",
      PhoneNumber = "044111111",
      PrivateEmail = "fake@gmail.com",
      Ethnicity = "Albanian",
      Nationality = "Kosovar"
    };
    builder.HasData(contactInfo);
  }
}
