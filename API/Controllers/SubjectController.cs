using API.DTOs.SubjectDtos;
using API.RequestFeatures;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class SubjectController : ApiController
{
  private SubjectService _subjectService;

  public SubjectController(SubjectService subjectService)
  {
    _subjectService = subjectService;
  }

  [HttpGet]
  public async Task<IActionResult> GetAllSubjects([FromQuery] SubjectParameters parameters, CancellationToken token)
  {
    var subjects = await _subjectService.GetAllSubjectsAsync(parameters, token);
    return Ok(subjects);
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> GetSubject(int id, CancellationToken token)
  {
    var subject = await _subjectService.GetSubjectAsync(id, token);
    return Ok(subject);
  }

  [HttpPost]
  public async Task<IActionResult> CreateSubject(AddSubjectDto subjectDto, CancellationToken token)
  {
    var subject = await _subjectService.CreateSubjectAsync(subjectDto, token);
    return Ok(subject);
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> UpdateSubject(int id, UpdateSubjectDto subjectDto, CancellationToken token)
  {
    var subject = await _subjectService.UpdateSubjectAsync(id, subjectDto, token);
    return Ok(subject);
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteSubject(int id, CancellationToken token)
  {
    await _subjectService.DeleteSubjectAsync(id, token);
    return Ok($"Successfully deleted subject with id: {id}.");
  }
}
