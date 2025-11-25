using API.Data;
using API.DTOs.SubjectDtos;
using API.Mappers;
using API.Models;
using API.RequestFeatures;
using API.Services.QueryExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace API.Services;

public class SubjectService
{
  private readonly ApplicationDbContext _context;
  private readonly RedisCacheService _cache;

  public SubjectService(ApplicationDbContext context, RedisCacheService cache)
  {
    _context = context;
    _cache = cache;
  }

  public async Task<PagedList<ViewSubjectDto>> GetAllSubjectsAsync(SubjectParameters subjectParameters,
    CancellationToken token)
  {
    var cached = _cache.GetData<IEnumerable<Subject>>("subjects");
    IEnumerable<Subject> subjects;
    if (cached is not null)
    {
      subjects = cached;
    }
    else
    {
      subjects = await _context.Subjects
        .Include(s => s.Professor)
        .AsNoTracking()
        .ToListAsync(token);
      _cache.SetData("subjects", subjects, 10);
    }
    var filteredSubjects = subjects.AsQueryable()
      .Filter(subjectParameters.ProfessorId, subjectParameters.FieldOfStudy)
      .Search(subjectParameters.SearchTerm)
      .Sort(subjectParameters.OrderBy);
    var count = filteredSubjects.Count();
    var pagedSubjects = filteredSubjects
      .Skip((subjectParameters.PageNumber - 1) * subjectParameters.PageSize)
      .Take(subjectParameters.PageSize);
    var subjectDtos = pagedSubjects.Select(s => s.ToDto()).ToList();
    
    return new PagedList<ViewSubjectDto>(subjectDtos, count, subjectParameters.PageNumber, subjectParameters.PageSize);
  }

  public async Task<ViewSubjectDto> GetSubjectAsync(int id, CancellationToken token)
  {
    var subject = await _context.Subjects
      .AsNoTracking()
      .Include(s => s.Professor)
      .FirstOrDefaultAsync(s => s.Id == id, token);
    if (subject is null) throw new Exception($"Subject with id: {id} not found.");

    return subject.ToDto();
  }

  public async Task<ViewSubjectDto> CreateSubjectAsync(AddSubjectDto dto, CancellationToken token)
  {
    var subject = dto.ToEntity();
    await _context.Subjects.AddAsync(subject, token);
    await _context.SaveChangesAsync(token);
    return subject.ToDto();
  }

  public async Task<ViewSubjectDto> UpdateSubjectAsync(int id, UpdateSubjectDto dto, CancellationToken token)
  {
    var subject = await _context.Subjects.FindAsync(id, token);
    if (subject is null) throw new Exception($"Subject with id: {id} not found.");

    dto.ToEntity(subject);
    await _context.SaveChangesAsync(token);
    return subject.ToDto();
  }

  public async Task DeleteSubjectAsync(int id, CancellationToken token)
  {
    var subject = await _context.Subjects.FindAsync(id, token);
    if (subject is null) throw new Exception($"Subject with id: {id} not found.");

    subject.IsDeleted = true;
    subject.DeletedAt = DateTime.UtcNow;
    await _context.SaveChangesAsync(token);
  }
}
