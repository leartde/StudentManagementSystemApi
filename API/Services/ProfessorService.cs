using API.Data;
using API.Models;
using API.Services.QueryExtensions;
using API.Shared.DTOs.ProfessorDtos;
using API.Shared.Mappers;
using API.Shared.RequestFeatures;
using API.Shared.Validators;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class ProfessorService
{
  private readonly ApplicationDbContext _context;
  private readonly IValidator<AddProfessorDto> _validator;
  public ProfessorService(ApplicationDbContext context)
  {
    _context = context;
    _validator = new AddProfessorValidator();
  }

  public async Task<Result<PagedList<ViewProfessorDto>>> GetAllProfessorsAsync(ProfessorParameters professorParameters,
    CancellationToken token)
  {
    var professors = _context.Professors
      .Search(professorParameters.SearchTerm)
      .Skip((professorParameters.PageNumber - 1) * professorParameters.PageSize)
      .Take(professorParameters.PageSize)
      .Include(p => p.ContactInfo)
      .Include(p => p.Subjects)
      .AsNoTracking();
    int count = _context.Professors
      .Search(professorParameters.SearchTerm)
      .AsNoTracking()
      .Count();
    var professorDtos = await professors.Select(p => p.ToDto()).ToListAsync(token);

    return Result<PagedList<ViewProfessorDto>>
      .Ok(new PagedList<ViewProfessorDto>(professorDtos, count, professorParameters.PageNumber,
        professorParameters.PageSize));
  }

  public async Task<Result<ViewProfessorDto>> GetProfessorAsync(int id, CancellationToken token)
  {
    var professor = await _context.Professors
      .Include(p => p.ContactInfo)
      .Include(p => p.Subjects)
      .AsNoTracking()
      .SingleOrDefaultAsync(p => p.Id == id, token);
    if (professor is null) return Result<ViewProfessorDto>.NotFound($"Professor with id: {id} not found");
    return Result<ViewProfessorDto>.Ok(professor.ToDto());
  }

  public async Task<Result<ViewProfessorDto>> CreateProfessorAsync(AddProfessorDto dto, CancellationToken token)
  {
    var validationResult = _validator.Validate(dto);
    if (!validationResult.Success)
    {
      return Result<ViewProfessorDto>.ValidationFail(validationResult.Errors);
    }
    var professor = dto.ToEntity();
    await _context.Professors.AddAsync(professor, token);
    await _context.SaveChangesAsync(token);
    return Result<ViewProfessorDto>.Ok(professor.ToDto());
  }

  public async Task<Result<ViewProfessorDto>> UpdateProfessorAsync(int id, UpdateProfessorDto dto,
    CancellationToken token)
  {
    var professor = await _context.Professors
      .Include(p => p.ContactInfo)
      .SingleOrDefaultAsync(p => p.Id == id, token);
    if (professor is null) return Result<ViewProfessorDto>.NotFound($"Professor with id: {id} not found");
    dto.ToEntity(professor);
    await _context.SaveChangesAsync(token);
    return Result<ViewProfessorDto>.Ok(professor.ToDto());
  }

  public async Task<Result<int>> DeleteProfessorAsync(int id, CancellationToken token)
  {
    var professor = await _context.Professors.FindAsync(id, token);
    if (professor is null) return Result<int>.NotFound($"Professor with id: {id} not found");
    ;
    professor.IsDeleted = true;
    professor.DeletedAt = DateTime.UtcNow;
    int affectedRows = await _context.SaveChangesAsync(token);
    return Result<int>.Ok(affectedRows);
  }
}
