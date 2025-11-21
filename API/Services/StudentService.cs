using API.Data;
using API.DTOs.StudentDtos;
using API.Mappers;
using API.RequestFeatures;
using API.Services.QueryExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class StudentService
{
  private readonly ApplicationDbContext _context;

  public StudentService(ApplicationDbContext context)
  {
    _context = context;
  }

  public async Task<IEnumerable<ViewStudentDto>> GetAllStudentsAsync(StudentParameters parameters,
    CancellationToken token)
  {
    var students = _context.Students
      .Filter(parameters.MinAverageGrade)
      .Search(parameters.SearchTerm)
      .Sort(parameters.OrderBy)
      .Skip((parameters.PageNumber - 1) * parameters.PageSize)
      .Take(parameters.PageSize)
      .Include(s => s.ContactInfo)
      .AsNoTracking();

    return await students.Select(s => s.ToDto()).ToListAsync(cancellationToken: token);
  }

  public async Task<ViewStudentDto> GetStudentAsync(int id, CancellationToken token)
  {
    var student = await _context.Students.Where(s => s.Id == id)
      .Include(s => s.ContactInfo)
      .AsNoTracking()
      .SingleOrDefaultAsync(cancellationToken: token);
    if (student is null) throw new Exception($"Student with id:{id} not found");
    return student.ToDto();
  }

  public async Task<ViewStudentDto> CreateStudentAsync(AddStudentDto studentDto, CancellationToken token)
  {
    var student = studentDto.ToEntity();
    await _context.Students.AddAsync(student, cancellationToken: token);
    await _context.SaveChangesAsync(cancellationToken: token);
    return student.ToDto();
  }

  public async Task DeleteStudentAsync(int id, CancellationToken token)
  {
    var student = await _context.Students.FindAsync(id);
    if (student is null) throw new Exception($"Student with id:{id} not found");
    student.IsDeleted = true;
    _context.Students.Update(student);
    await _context.SaveChangesAsync(cancellationToken: token);
  }
}
