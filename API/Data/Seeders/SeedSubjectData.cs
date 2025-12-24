using API.Enums;
using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Data.Seeders;

public class SeedSubjectData : IEntityTypeConfiguration<Subject>
{
  public static readonly List<Subject> Subjects = new();
  private static readonly string[] _commonSubjects = ["Criminal Law", "Data Structures and Algorithms",
  "Chemistry", "Anatomy", "Design", "Social Psychology", "Thermodynamics"];
  public void Configure(EntityTypeBuilder<Subject> builder)
  {
    for (int i = 0; i < 7; i++)
    {
      var name = _commonSubjects[i];
      Subjects.Add(new Subject
      {
        Id = i + 1,
        Title = name,
        Code = name.ToUpper()[..3] + "101",
        ECTS = 4,
        IsObligatory = true,
        FieldOfStudy = (FieldOfStudy)(i + 1),
        ProfessorId = SeedProfessorData.Professors[i].Id,
      });
    }

    builder.HasData(Subjects);
  }
}
