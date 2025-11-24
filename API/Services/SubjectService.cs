using API.Data;
using API.DTOs.SubjectDtos;
using API.Mappers;
using API.RequestFeatures;
using API.Services.QueryExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class SubjectService
{
  private readonly ApplicationDbContext _context;

  public SubjectService(ApplicationDbContext context)
  {
    _context = context;
  }

  public async Task<IEnumerable<ViewSubjectDto>> GetAllSubjectsAsync(SubjectParameters subjectParameters,CancellationToken token)
  {
    var subjects =  _context.Subjects
      .Filter(subjectParameters.ProfessorId, subjectParameters.FieldOfStudy)
      .Search(subjectParameters.SearchTerm)
      .Sort(subjectParameters.OrderBy)
      .Skip((subjectParameters.PageNumber - 1) * subjectParameters.PageSize)
      .Take(subjectParameters.PageSize)
      .Include(s => s.Professor)
        .AsNoTracking();
    return await subjects.Select(s => s.ToDto()).ToListAsync(token);
  }

  public async Task<ViewSubjectDto> GetSubjectAsync(int id, CancellationToken token)
  {
    var subject = await _context.Subjects
      .AsNoTracking()
      .Include(s => s.Professor)
      .FirstOrDefaultAsync(s => s.Id == id, token);
    if (subject is null)
    {
      throw new Exception($"Subject with id: {id} not found.");
    }
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
    if (subject is null)
    {
      throw new Exception($"Subject with id: {id} not found.");
    }
    dto.ToEntity(subject);
    await _context.SaveChangesAsync(token);
    return subject.ToDto();
  }

  public async Task DeleteSubjectAsync(int id, CancellationToken token)
  {
    var subject = await _context.Subjects.FindAsync(id, token);
    if (subject is null)
    {
      throw new Exception($"Subject with id: {id} not found.");
    }
    subject.IsDeleted = true;
    subject.DeletedAt = DateTime.UtcNow;
    await _context.SaveChangesAsync(token);
  }
}
