using API.Data;
using API.Models;
using API.Services.QueryExtensions;
using API.Shared.DTOs.GradeDtos;
using API.Shared.Mappers;
using API.Shared.RequestFeatures;
using API.Shared.Validators;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class GradeService
{
  private readonly ApplicationDbContext _context;
  private readonly IValidator<AddGradeDto> _validator;

  public GradeService(ApplicationDbContext context)
  {
    _context = context;
    _validator = new AddGradeValidator();
  }

  public async Task<Result<PagedList<ViewGradeDto>>> GetAllGradesAsync(GradeParameters gradeParameters,
    CancellationToken token)
  {
    var grades = _context.Grades
      .Filter(gradeParameters.StudentId, gradeParameters.SubjectId)
      .Sort(gradeParameters.OrderBy)
      .Skip((gradeParameters.PageNumber - 1) * gradeParameters.PageSize)
      .Take(gradeParameters.PageSize)
      .Include(g => g.Student)
      .Include(g => g.Subject)
      .AsNoTracking();
    int count = _context.Grades
      .Filter(gradeParameters.StudentId, gradeParameters.SubjectId)
      .AsNoTracking()
      .Count();
    var gradeDtos = await grades.Select(g => g.ToDto()).ToListAsync(token);

    return Result<PagedList<ViewGradeDto>>
      .Ok(new PagedList<ViewGradeDto>(gradeDtos, count, gradeParameters.PageNumber,
        gradeParameters.PageSize));
  }

  public async Task<Result<ViewGradeDto>> GetGradeAsync(int studentId, int subjectId, CancellationToken token)
  {
    var grade = await _context.Grades
      .Include(g => g.Student)
      .Include(g => g.Subject)
      .AsNoTracking()
      .SingleOrDefaultAsync(g => g.StudentId == studentId && g.SubjectId == subjectId, token);
    if (grade is null) return Result<ViewGradeDto>.NotFound("Grade  not found");
    return Result<ViewGradeDto>.Ok(grade.ToDto());
  }

  public async Task<Result<ViewGradeDto>> CreateGradeAsync(AddGradeDto dto, CancellationToken token)
  {
    var validationResult = _validator.Validate(dto);
    if(!validationResult.Success)
    {
      return Result<ViewGradeDto>.ValidationFail(validationResult.Errors);
    }
    var grade = dto.ToEntity();
    await _context.Grades.AddAsync(grade, token);
    await _context.SaveChangesAsync(token);
    return Result<ViewGradeDto>.Ok(grade.ToDto());
  }

  public async Task<Result<ViewGradeDto>> UpdateGradeAsync(int studentId, int subjectId, UpdateGradeDto dto,
    CancellationToken token)
  {
    var grade = await _context.Grades
      .AsNoTracking()
      .SingleOrDefaultAsync(g => g.StudentId == studentId && g.SubjectId == subjectId, token);
    if (grade is null) return Result<ViewGradeDto>.NotFound("Grade not found");
    dto.ToEntity(grade);
    await _context.SaveChangesAsync(token);
    return Result<ViewGradeDto>.Ok(grade.ToDto());
  }

  public async Task<Result<int>> DeleteGradeAsync(int studentId, int subjectId, CancellationToken token)
  {
    var grade = await _context.Grades
      .AsNoTracking()
      .SingleOrDefaultAsync(g => g.StudentId == studentId && g.SubjectId == subjectId, token);
    if (grade is null) return Result<int>.NotFound("Grade not found");
    grade.IsDeleted = true;
    grade.DeletedAt = DateTime.UtcNow;
    int affectedRows = await _context.SaveChangesAsync(token);
    return Result<int>.Ok(affectedRows);
  }
}
