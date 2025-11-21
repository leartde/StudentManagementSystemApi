using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class ApplicationDbContext : DbContext
{
  public ApplicationDbContext(DbContextOptions options) : base(options){}
  public DbSet<Student> Students { get; set; } = null!;
  public DbSet<Professor> Professors { get; set; } = null!;
  public DbSet<Subject> Subjects { get; set; } = null!;
  public DbSet<Grade> Grades { get; set; } = null!;
  public DbSet<ContactInfo> ContactInfos { get; set; } = null!;

}
