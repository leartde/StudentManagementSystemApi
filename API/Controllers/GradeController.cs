using API.DTOs.GradeDtos;
using API.RequestFeatures;
using API.Services;
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
  public async Task<IActionResult> GetAllGrades([FromQuery] GradeParameters gradeParameters,CancellationToken token)
  {
    var grades = await _gradeService.GetAllGradesAsync(gradeParameters, token);
    return Ok(grades);
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> GetGrade(int id, CancellationToken token)
  {
    var grade = await _gradeService.GetGradeAsync(id, token);
    return Ok(grade);
  }

  [HttpPost]
  public async Task<IActionResult> CreateGrade(AddGradeDto gradeDto, CancellationToken token)
  {
    var grade = await _gradeService.CreateGradeAsync(gradeDto, token);
    return Ok(grade);
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> UpdateGrade(int id, UpdateGradeDto gradeDto, CancellationToken token)
  {
    var grade = await _gradeService.UpdateGradeAsync(id, gradeDto, token);
    return Ok(grade);
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteGrade(int id, CancellationToken token)
  {
    await _gradeService.DeleteGradeAsync(id, token);
    return Ok($"Grade with id: {id} successfully deleted");
  }
}
