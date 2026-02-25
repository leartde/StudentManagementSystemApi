using API.Data;
using API.Models;
using API.Services.QueryExtensions;
using API.Shared.DTOs.SubjectDtos;
using API.Shared.Mappers;
using API.Shared.RequestFeatures;
using API.Shared.Validators;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class SubjectService
{
  private readonly RedisCacheService _cache;
  private readonly ApplicationDbContext _context;
  private readonly IValidator<AddSubjectDto> _validator;

  public SubjectService(ApplicationDbContext context, RedisCacheService cache)
  {
    _context = context;
    _cache = cache;
    _validator = new AddSubjectValidator();
  }

  public async Task<Result<PagedList<ViewSubjectDto>>> GetAllSubjectsAsync(SubjectParameters subjectParameters,
    CancellationToken token)
  {
    var subjects = _context.Subjects
      .Filter(subjectParameters.ProfessorId, subjectParameters.FieldOfStudy)
      .Search(subjectParameters.SearchTerm)
      .Sort(subjectParameters.OrderBy)
      .Skip((subjectParameters.PageNumber - 1) * subjectParameters.PageSize)
      .Take(subjectParameters.PageSize)
      .Include(s => s.Professor)
      .AsNoTracking();

    int count = _context.Subjects
      .Include(s => s.Professor)
      .Filter(subjectParameters.ProfessorId, subjectParameters.FieldOfStudy)
      .Search(subjectParameters.SearchTerm)
      .AsNoTracking()
      .Count();
    var subjectDtos = await subjects.Select(s => s.ToDto()).ToListAsync(token);
    return Result<PagedList<ViewSubjectDto>>
      .Ok(new PagedList<ViewSubjectDto>(subjectDtos, count, subjectParameters.PageNumber, subjectParameters.PageSize));
  }

  public async Task<Result<ViewSubjectDto>> GetSubjectAsync(int id, CancellationToken token)
  {
    var subject = await _context.Subjects
      .AsNoTracking()
      .Include(s => s.Professor)
      .FirstOrDefaultAsync(s => s.Id == id, token);
    if (subject is null) return Result<ViewSubjectDto>.NotFound($"Subject with id: {id} not found.");

    return Result<ViewSubjectDto>.Ok(subject.ToDto());
  }

  public async Task<Result<ViewSubjectDto>> CreateSubjectAsync(AddSubjectDto dto, CancellationToken token)
  {
    var validationResult = _validator.Validate(dto);
    if (!validationResult.Success)
    {
      return Result<ViewSubjectDto>.ValidationFail(validationResult.Errors);
    }

    var professor = await _context.Professors.FindAsync(dto.ProfessorId);
    if (professor is null)
    {
      return Result<ViewSubjectDto>
        .Conflict($"Foreign key conflict. Professor with id: {dto.ProfessorId} not found.");
    }
    var subject = dto.ToEntity();
    await _context.Subjects.AddAsync(subject, token);
    await _context.SaveChangesAsync(token);
    return Result<ViewSubjectDto>.Ok(subject.ToDto());
  }

  public async Task<Result<ViewSubjectDto>> UpdateSubjectAsync(int id, UpdateSubjectDto dto, CancellationToken token)
  {
    var subject = await _context.Subjects.FindAsync(id, token);
    if (subject is null) return Result<ViewSubjectDto>.NotFound($"Subject with id: {id} not found.");

    dto.ToEntity(subject);
    await _context.SaveChangesAsync(token);
    return Result<ViewSubjectDto>.Ok(subject.ToDto());
  }

  public async Task<Result<int>> DeleteSubjectAsync(int id, CancellationToken token)
  {
    var subject = await _context.Subjects.FindAsync(id, token);
    if (subject is null) return Result<int>.NotFound($"Subject with id: {id} not found.");
    subject.IsDeleted = true;
    subject.DeletedAt = DateTime.UtcNow;
    int affectedRows = await _context.SaveChangesAsync(token);
    return Result<int>.Ok(affectedRows);
  }
}
