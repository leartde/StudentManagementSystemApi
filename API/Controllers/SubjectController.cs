using System.Text.Json;
using API.Services;
using API.Shared.DTOs.SubjectDtos;
using API.Shared.RequestFeatures;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class SubjectController : ApiController
{
  private readonly SubjectService _subjectService;

  public SubjectController(SubjectService subjectService)
  {
    _subjectService = subjectService;
  }

  [HttpGet]
  public async Task<IActionResult> GetAllSubjects([FromQuery] SubjectParameters parameters, CancellationToken token)
  {
    var result = await _subjectService.GetAllSubjectsAsync(parameters, token);
    Response.Headers["X-Pagination"] = JsonSerializer.Serialize(result.Value.MetaData);
    return Ok(result);
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> GetSubject(int id, CancellationToken token)
  {
    var result = await _subjectService.GetSubjectAsync(id, token);
    return Ok(result);
  }

  [HttpPost]
  public async Task<IActionResult> CreateSubject(AddSubjectDto subjectDto, CancellationToken token)
  {
    var result = await _subjectService.CreateSubjectAsync(subjectDto, token);
    return Ok(result);
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> UpdateSubject(int id, UpdateSubjectDto subjectDto, CancellationToken token)
  {
    var result = await _subjectService.UpdateSubjectAsync(id, subjectDto, token);
    return Ok(result);
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteSubject(int id, CancellationToken token)
  {
    await _subjectService.DeleteSubjectAsync(id, token);
    return Ok($"Successfully deleted subject with id: {id}.");
  }
}
