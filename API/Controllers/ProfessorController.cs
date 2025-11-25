using System.Text.Json;
using API.DTOs.ProfessorDtos;
using API.RequestFeatures;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProfessorController : ApiController
{
  private readonly ProfessorService _professorService;

  public ProfessorController(ProfessorService professorService)
  {
    _professorService = professorService;
  }

  [HttpGet]
  public async Task<IActionResult> GetAllProfessors([FromQuery] ProfessorParameters parameters, CancellationToken token)
  {
    var professors = await _professorService.GetAllProfessorsAsync(parameters, token);
    Response.Headers["X-Pagination"] = JsonSerializer.Serialize(professors.MetaData);
    return Ok(professors);
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> GetProfessor(int id, CancellationToken token)
  {
    var professor = await _professorService.GetProfessorAsync(id, token);
    return Ok(professor);
  }

  [HttpPost]
  public async Task<IActionResult> CreateProfessor(AddProfessorDto professorDto, CancellationToken token)
  {
    var professor = await _professorService.CreateProfessorAsync(professorDto, token);
    return Ok(professor);
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> UpdateProfessor(int id, UpdateProfessorDto professorDto, CancellationToken token)
  {
    var professor = await _professorService.UpdateProfessorAsync(id, professorDto, token);
    return Ok(professor);
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteProfessor(int id, CancellationToken token)
  {
    await _professorService.DeleteProfessorAsync(id, token);
    return Ok($"Professor with id: {id} successfully deleted.");
  }
}
