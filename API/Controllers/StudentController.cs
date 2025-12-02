using System.Text.Json;
using API.Services;
using API.Shared.DTOs.StudentDtos;
using API.Shared.RequestFeatures;
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
    var result = await _studentService.GetAllStudentsAsync(parameters, token);
    Response.Headers["X-Pagination"] = JsonSerializer.Serialize(result.Value.MetaData);
    return Ok(result);
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> GetStudent(int id, CancellationToken token)
  {
    var result = await _studentService.GetStudentAsync(id, token);
    return Ok(result);
  }

  [HttpPost]
  public async Task<IActionResult> CreateStudent(AddStudentDto studentDto, CancellationToken token)
  {
    var result = await _studentService.CreateStudentAsync(studentDto, token);
    return Ok(result);
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> UpdateStudent(int id, UpdateStudentDto studentDto, CancellationToken token)
  {
    var result = await _studentService.UpdateStudentAsync(id, studentDto, token);
    return Ok(result);
  }

  [HttpDelete]
  public async Task<IActionResult> DeleteStudent(int id, CancellationToken token)
  {
    var result = await _studentService.DeleteStudentAsync(id, token);
    return Ok(result);
  }
}
