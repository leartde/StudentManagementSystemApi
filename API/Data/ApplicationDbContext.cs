using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class ApplicationDbContext : DbContext
{
  public ApplicationDbContext(DbContextOptions options) : base(options)
  {
  }

  public DbSet<Student> Students { get; set; } = null!;
  public DbSet<Professor> Professors { get; set; } = null!;
  public DbSet<Subject> Subjects { get; set; } = null!;
  public DbSet<Grade> Grades { get; set; } = null!;
  public DbSet<ContactInfo> ContactInfos { get; set; } = null!;

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Student>()
      .HasQueryFilter(s => !s.IsDeleted);
    modelBuilder.Entity<Subject>()
      .HasQueryFilter(s => !s.IsDeleted);
    modelBuilder.Entity<Professor>()
      .HasQueryFilter(p => !p.IsDeleted);
    modelBuilder.Entity<Grade>()
      .HasQueryFilter(g => !g.IsDeleted);
    modelBuilder.Entity<ContactInfo>()
      .HasQueryFilter(c => !c.IsDeleted);


    modelBuilder.Entity<Grade>()
      .HasKey(g => new { g.StudentId, g.SubjectId });

    modelBuilder.Entity<Grade>()
      .HasOne(g => g.Student)
      .WithMany()
      .HasForeignKey(g => g.StudentId)
      .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<Grade>()
      .HasOne(g => g.Subject)
      .WithMany()
      .HasForeignKey(g => g.SubjectId)
      .OnDelete(DeleteBehavior.Cascade);
  }
}
