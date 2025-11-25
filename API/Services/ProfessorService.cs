using API.Data;
using API.DTOs.ProfessorDtos;
using API.Mappers;
using API.RequestFeatures;
using API.Services.QueryExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class ProfessorService
{
  private readonly ApplicationDbContext _context;

  public ProfessorService(ApplicationDbContext context)
  {
    _context = context;
  }

  public async Task<PagedList<ViewProfessorDto>> GetAllProfessorsAsync(ProfessorParameters professorParameters,
    CancellationToken token)
  {
    var professors = _context.Professors
      .Search(professorParameters.SearchTerm)
      .Skip((professorParameters.PageNumber - 1) * professorParameters.PageSize)
      .Take(professorParameters.PageSize)
      .Include(p => p.ContactInfo)
      .Include(p => p.Subjects)
      .AsNoTracking();
    var count = _context.Professors
      .Search(professorParameters.SearchTerm)
      .AsNoTracking()
      .Count();
    var professorDtos = await professors.Select(p => p.ToDto()).ToListAsync(token);

    return new PagedList<ViewProfessorDto>(professorDtos, count, professorParameters.PageNumber,
      professorParameters.PageSize);
  }

  public async Task<ViewProfessorDto> GetProfessorAsync(int id, CancellationToken token)
  {
    var professor = await _context.Professors
      .Include(p => p.ContactInfo)
      .Include(p => p.Subjects)
      .AsNoTracking()
      .SingleOrDefaultAsync(p => p.Id == id, token);
    if (professor is null) throw new Exception($"Cannot find professor with id: {id}");
    return professor.ToDto();
  }

  public async Task<ViewProfessorDto> CreateProfessorAsync(AddProfessorDto dto, CancellationToken token)
  {
    var professor = dto.ToEntity();
    await _context.Professors.AddAsync(professor, token);
    await _context.SaveChangesAsync(token);
    return professor.ToDto();
  }

  public async Task<ViewProfessorDto> UpdateProfessorAsync(int id, UpdateProfessorDto dto, CancellationToken token)
  {
    var professor = await _context.Professors
      .Include(p => p.ContactInfo)
      .SingleOrDefaultAsync(p => p.Id == id, token);
    if (professor is null) throw new Exception($"Cannot find professor with id: {id}");
    dto.ToEntity(professor);
    await _context.SaveChangesAsync(token);
    return professor.ToDto();
  }

  public async Task DeleteProfessorAsync(int id, CancellationToken token)
  {
    var professor = await _context.Professors.FindAsync(id, token);
    if (professor is null) throw new Exception($"Cannot find professor with id: {id}");
    professor.IsDeleted = true;
    professor.DeletedAt = DateTime.UtcNow;
    await _context.SaveChangesAsync(token);
  }
}
