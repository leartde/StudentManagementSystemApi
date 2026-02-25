using API.Data;
using API.Models;
using API.Services.QueryExtensions;
using API.Shared.DTOs.StudentDtos;
using API.Shared.Mappers;
using API.Shared.RequestFeatures;
using API.Shared.Validators;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class StudentService
{
  private readonly RedisCacheService _cache;
  private readonly ApplicationDbContext _context;
  private readonly IValidator<AddStudentDto> _validator;

  public StudentService(ApplicationDbContext context, RedisCacheService cache)
  {
    _context = context;
    _cache = cache;
    _validator = new AddStudentValidator();
  }

  public async Task<Result<PagedList<ViewStudentDto>>> GetAllStudentsAsync(StudentParameters studentParameters,
    CancellationToken token)
  {
    string cacheKey = $"students_{studentParameters.MinAverageGrade}_{studentParameters.FieldOfStudy}" +
                      $"_{studentParameters.SearchTerm}_{studentParameters.OrderBy}" +
                      $"_{studentParameters.PageNumber}_{studentParameters.PageSize}";
    var cached = _cache.GetData<PagedList<ViewStudentDto>>("cacheKey");
    if (cached is not null) return Result<PagedList<ViewStudentDto>>.Ok(cached);

    var students = _context.Students
      .Include(s => s.ContactInfo)
      .Include(s => s.Grades)
      .Filter(studentParameters.MinAverageGrade, studentParameters.FieldOfStudy)
      .Search(studentParameters.SearchTerm)
      .Sort(studentParameters.OrderBy)
      .Skip((studentParameters.PageNumber - 1) * studentParameters.PageSize)
      .Take(studentParameters.PageSize)
      .AsNoTracking();
    int count = _context.Students
      .Filter(studentParameters.MinAverageGrade, studentParameters.FieldOfStudy)
      .Search(studentParameters.SearchTerm)
      .AsNoTracking()
      .Count();
    var studentDtos = await students.Select(s => s.ToDto()).ToListAsync(token);
    var result = new PagedList<ViewStudentDto>(studentDtos, count,
      studentParameters.PageNumber, studentParameters.PageSize);
    _cache.SetData(cacheKey, result, 5);
    return Result<PagedList<ViewStudentDto>>
      .Ok(result);
  }

  public async Task<Result<ViewStudentDto>> GetStudentAsync(int id, CancellationToken token)
  {
    var student = await _context.Students
      .Include(s => s.ContactInfo)
      .Include(s => s.Grades)
      .AsNoTracking()
      .SingleOrDefaultAsync(s => s.Id == id, token);
    if (student is null) return Result<ViewStudentDto>.NotFound($"Couldn't find student with id: {id}");
    return Result<ViewStudentDto>.Ok(student.ToDto());
  }

  public async Task<Result<ViewStudentDto>> CreateStudentAsync(AddStudentDto studentDto, CancellationToken token)
  {
    var validationResult = _validator.Validate(studentDto);
    if (!validationResult.Success)
    {
      return Result<ViewStudentDto>.ValidationFail(validationResult.Errors);
    }
    string publicId = await GeneratePublicIdAsync(studentDto.RegistrationYear);
    var student = studentDto.ToEntity();
    student.PublicId = publicId;
    await _context.Students.AddAsync(student, token);
    await _context.SaveChangesAsync(token);
    return Result<ViewStudentDto>.Ok(student.ToDto());
  }

  public async Task<Result<ViewStudentDto>> UpdateStudentAsync(int id, UpdateStudentDto studentDto,
    CancellationToken token)
  {
    var student = await _context.Students
      .Include(s => s.ContactInfo)
      .SingleOrDefaultAsync(s => s.Id == id, token);
    if (student is null) return Result<ViewStudentDto>.NotFound($"Couldn't find student with id: {id}");
    studentDto.ToEntity(student);
    await _context.SaveChangesAsync(token);
    return Result<ViewStudentDto>.Ok(student.ToDto());
  }

  public async Task<Result<int>> DeleteStudentAsync(int id, CancellationToken token)
  {
    var student = await _context.Students.SingleOrDefaultAsync(s => s.Id == id, token);
    if (student is null) return Result<int>.NotFound($"Couldn't find student with id: {id}");
    student.IsDeleted = true;
    student.DeletedAt = DateTime.UtcNow;
    int affectedRows = await _context.SaveChangesAsync(token);
    return Result<int>.Ok(affectedRows);
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
