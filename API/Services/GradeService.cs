using API.Data;
using API.DTOs.GradeDtos;
using API.Mappers;
using API.RequestFeatures;
using API.Services.QueryExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class GradeService
{
  private readonly ApplicationDbContext _context;

  public GradeService(ApplicationDbContext context)
  {
    _context = context;
  }

  public async Task<IEnumerable<ViewGradeDto>> GetAllGradesAsync(GradeParameters gradeParameters, CancellationToken token)
  {
    var grades = _context.Grades
      .Filter(gradeParameters.StudentId, gradeParameters.SubjectId)
      .Sort(gradeParameters.OrderBy)
      .Skip((gradeParameters.PageNumber - 1) * gradeParameters.PageSize)
      .Take(gradeParameters.PageSize)
      .Include(g => g.Student)
      .Include(g => g.Subject)
      .AsNoTracking();
    return await grades.Select(g => g.ToDto()).ToListAsync(token);
  }

  public async Task<ViewGradeDto> GetGradeAsync(int id, CancellationToken token)
  {
    var grade = await _context.Grades
      .Include(g => g.Student)
      .Include(g => g.Subject)
      .AsNoTracking()
      .SingleOrDefaultAsync(g => g.Id == id, token);
    if (grade is null)
    {
      throw new Exception($"Grade with id: {id} not found.");
    }
    return grade.ToDto();
  }

  public async Task<ViewGradeDto> CreateGradeAsync(AddGradeDto dto, CancellationToken token)
  {
    var grade = dto.ToEntity();
    await _context.Grades.AddAsync(grade, token);
    await _context.SaveChangesAsync(token);
    return grade.ToDto();
  }

  public async Task<ViewGradeDto> UpdateGradeAsync(int id, UpdateGradeDto dto, CancellationToken token)
  {
    var grade = await _context.Grades.FindAsync(id, token);
    if (grade is null)
    {
      throw new Exception($"Grade with id: {id} not found.");
    }
    dto.ToEntity(grade);
    await _context.SaveChangesAsync(token);
    return grade.ToDto();
  }

  public async Task DeleteGradeAsync(int id, CancellationToken token)
  {
    var grade = await _context.Grades.FindAsync(id, token);
    if (grade is null)
    {
      throw new Exception($"Grade with id: {id} not found.");
    }
    grade.IsDeleted = true;
    grade.DeletedAt = DateTime.UtcNow;
    await _context.SaveChangesAsync(token);
  }
}
