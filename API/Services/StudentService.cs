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
      .Filter(parameters.MinAverageGrade, parameters.FieldOfStudy)
      .Search(parameters.SearchTerm)
      .Sort(parameters.OrderBy)
      .Skip((parameters.PageNumber - 1) * parameters.PageSize)
      .Take(parameters.PageSize)
      .Include(s => s.ContactInfo)
      .AsNoTracking();

    return await students.Select(s => s.ToDto()).ToListAsync(token);
  }

  public async Task<ViewStudentDto> GetStudentAsync(int id, CancellationToken token)
  {
    var student = await _context.Students
      .Include(s => s.ContactInfo)
      .AsNoTracking()
      .SingleOrDefaultAsync(s => s.Id == id, token);
    if (student is null) throw new Exception($"Student with id:{id} not found");
    return student.ToDto();
  }

  public async Task<ViewStudentDto> CreateStudentAsync(AddStudentDto studentDto, CancellationToken token)
  {
    var publicId = await GeneratePublicIdAsync(studentDto.RegistrationYear);
    var student = studentDto.ToEntity();
    student.PublicId = publicId;
    await _context.Students.AddAsync(student, token);
    await _context.SaveChangesAsync(token);
    return student.ToDto();
  }

  public async Task<ViewStudentDto> UpdateStudentAsync(int id, UpdateStudentDto studentDto, CancellationToken token)
  {
    var student = await _context.Students
      .Include(s => s.ContactInfo)
      .SingleOrDefaultAsync(s => s.Id == id, token);
    if (student is null)
    {
      throw new Exception($"Student with id:{id} not found");
    }

    studentDto.ToEntity(student);
    await _context.SaveChangesAsync(token);
    return student.ToDto();
  }

  public async Task DeleteStudentAsync(int id, CancellationToken token)
  {
    var student = await _context.Students.FindAsync(id, token);
    if (student is null) throw new Exception($"Student with id:{id} not found");
    student.IsDeleted = true;
    student.DeletedAt = DateTime.UtcNow;
    await _context.SaveChangesAsync(token);
  }

  private async Task<string> GeneratePublicIdAsync(int registrationYear)
  {
    string publicId;
    bool isUnique;
    var random = new Random();
    do
    {
      var yearPart = (registrationYear % 100).ToString("00");
      var nextYearPart = ((registrationYear + 1) % 100).ToString("00");
      var randomPart = random.Next(0, 100000).ToString("00000");
      publicId = $"{yearPart}{nextYearPart}{randomPart}";
      isUnique = !await _context.Students.AnyAsync(s => s.PublicId == publicId);
    } while (!isUnique);

    return publicId;
  }
}
