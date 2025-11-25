using API.Data;
using API.DTOs.StudentDtos;
using API.Mappers;
using API.Models;
using API.RequestFeatures;
using API.Services.QueryExtensions;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class StudentService
{
  private readonly ApplicationDbContext _context;
  private readonly RedisCacheService _cache;
  public StudentService(ApplicationDbContext context, RedisCacheService cache)
  {
    _context = context;
    _cache = cache;
  }

  public async Task<PagedList<ViewStudentDto>> GetAllStudentsAsync(StudentParameters studentParameters,
    CancellationToken token)
  {
    var cached = _cache.GetData<IEnumerable<Student>>("students");
    IEnumerable<Student> students;
    if (cached is not null)
    {
      students = cached;
    }
    else
    {
      students = await _context.Students
        .Include(s => s.ContactInfo)
        .AsNoTracking()
        .ToListAsync(token);
      _cache.SetData("students", students, 5);
    }

    var filteredStudents = students.AsQueryable()
      .Filter(studentParameters.MinAverageGrade, studentParameters.FieldOfStudy)
      .Search(studentParameters.SearchTerm)
      .Sort(studentParameters.OrderBy);
    var count = filteredStudents.Count();
    var pagedStudents = filteredStudents
      .Skip((studentParameters.PageNumber - 1) * studentParameters.PageSize)
      .Take(studentParameters.PageSize);
    var studentDtos =  pagedStudents.Select(s => s.ToDto()).ToList();
    
    return new PagedList<ViewStudentDto>(studentDtos, count, studentParameters.PageNumber, studentParameters.PageSize);
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
    string publicId = await GeneratePublicIdAsync(studentDto.RegistrationYear);
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
    if (student is null) throw new Exception($"Student with id:{id} not found");

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
