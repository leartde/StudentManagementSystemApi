using API.Data;
using API.DTOs.ProfessorDtos;
using API.Mappers;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class ProfessorService
{
  private readonly ApplicationDbContext _context;

  public ProfessorService(ApplicationDbContext context)
  {
    _context = context;
  }

  public async Task<IEnumerable<ViewProfessorDto>> GetAllProfessorsAsync(CancellationToken token)
  {
    var professors =  _context.Professors
      .Include(p => p.ContactInfo)
      .Include(p => p.Subjects)
      .AsNoTracking();
    return await professors.Select(p => p.ToDto()).ToListAsync(token);
  }

  public async Task<ViewProfessorDto> GetProfessorAsync(int id, CancellationToken token)
  {
    var professor = await _context.Professors
      .Include(p => p.ContactInfo)
      .Include(p => p.Subjects)
      .AsNoTracking()
      .SingleOrDefaultAsync(p => p.Id == id, token);
    if (professor is null)
    {
      throw new Exception($"Cannot find professor with id: {id}");
    }
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
    if (professor is null)
    {
      throw new Exception($"Cannot find professor with id: {id}");
    }
    dto.ToEntity(professor);
    await _context.SaveChangesAsync(token);
    return professor.ToDto();
  }

  public async Task DeleteProfessorAsync(int id, CancellationToken token)
  {
    var professor = await _context.Professors.FindAsync(id, token);
    if (professor is null)
    {
      throw new Exception($"Cannot find professor with id: {id}");
    }
    professor.IsDeleted = true;
    professor.DeletedAt = DateTime.UtcNow;
    await _context.SaveChangesAsync(token);
  }
}
