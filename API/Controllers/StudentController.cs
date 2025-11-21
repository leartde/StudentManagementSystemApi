using API.DTOs.StudentDtos;
using API.RequestFeatures;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class StudentController : ApiController
{
  private readonly StudentService _studentService;

  public StudentController(StudentService studentService)
  {
    _studentService = studentService;
  }

  [HttpGet]
  public async Task<IActionResult> GetStudents([FromQuery] StudentParameters parameters, CancellationToken token)
  {
    var students = await _studentService.GetAllStudentsAsync(parameters, token);
    return Ok(students);
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> GetStudent(int id, CancellationToken token)
  {
    var student = await _studentService.GetStudentAsync(id, token);
    return Ok(student);
  }

  [HttpPost]
  public async Task<IActionResult> CreateStudent(AddStudentDto studentDto, CancellationToken token)
  {
    var studentToAdd = await _studentService.CreateStudentAsync(studentDto, token);
    return Ok(studentToAdd);
  }

  [HttpDelete]
  public async Task<IActionResult> DeleteStudent(int id, CancellationToken token)
  {
    await _studentService.DeleteStudentAsync(id, token);
    return Ok();
  }
}
