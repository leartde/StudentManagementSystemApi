using System.Text.Json;
using API.Services;
using API.Shared.DTOs.GradeDtos;
using API.Shared.RequestFeatures;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class GradeController : ApiController
{
  private readonly GradeService _gradeService;

  public GradeController(GradeService gradeService)
  {
    _gradeService = gradeService;
  }

  [HttpGet]
  public async Task<IActionResult> GetAllGrades([FromQuery] GradeParameters gradeParameters, CancellationToken token)
  {
    var result = await _gradeService.GetAllGradesAsync(gradeParameters, token);
    Response.Headers["X-Pagination"] = JsonSerializer.Serialize(result.Value.MetaData);
    return Ok(result);
  }

  [HttpPost]
  public async Task<IActionResult> CreateGrade(AddGradeDto gradeDto, CancellationToken token)
  {
    var result = await _gradeService.CreateGradeAsync(gradeDto, token);
    return Ok(result);
  }

  [HttpPut("{studentId}/{subjectId}")]
  public async Task<IActionResult> UpdateGrade(int studentId, int subjectId, UpdateGradeDto gradeDto,
    CancellationToken token)
  {
    var result = await _gradeService.UpdateGradeAsync(studentId, subjectId, gradeDto, token);
    return Ok(result);
  }

  [HttpDelete("{studentId}/{subjectId}")]
  public async Task<IActionResult> DeleteGrade(int studentId, int subjectId, CancellationToken token)
  {
    await _gradeService.DeleteGradeAsync(studentId, subjectId, token);
    return Ok($"Grade with student id: {studentId} and subject id: {subjectId} successfully deleted");
  }
}
