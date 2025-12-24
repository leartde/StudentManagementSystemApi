using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Data.Seeders;

public class SeedGradeData : IEntityTypeConfiguration<Grade>
{
  public static readonly List<Grade> Grades = [];
  public void Configure(EntityTypeBuilder<Grade> builder)
  {
    for (int i = 0; i < 100; i++)
    {
      Grades.Add(new Grade
      {
        StudentId = SeedStudentData.Students[i].Id,
        SubjectId = SeedSubjectData.Subjects[i % 7].Id,
        Mark = (byte)(i % 5 + 6)
      });
    }

    builder.HasData(Grades);
  }
}
