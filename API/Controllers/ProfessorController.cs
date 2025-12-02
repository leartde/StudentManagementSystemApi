using System.Text.Json;
using API.Services;
using API.Shared.DTOs.ProfessorDtos;
using API.Shared.RequestFeatures;
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
    var result = await _professorService.GetAllProfessorsAsync(parameters, token);
    Response.Headers["X-Pagination"] = JsonSerializer.Serialize(result.Value.MetaData);
    return Ok(result);
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> GetProfessor(int id, CancellationToken token)
  {
    var result = await _professorService.GetProfessorAsync(id, token);
    return Ok(result);
  }

  [HttpPost]
  public async Task<IActionResult> CreateProfessor(AddProfessorDto professorDto, CancellationToken token)
  {
    var result = await _professorService.CreateProfessorAsync(professorDto, token);
    return Ok(result);
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> UpdateProfessor(int id, UpdateProfessorDto professorDto, CancellationToken token)
  {
    var result = await _professorService.UpdateProfessorAsync(id, professorDto, token);
    return Ok(result);
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteProfessor(int id, CancellationToken token)
  {
    await _professorService.DeleteProfessorAsync(id, token);
    return Ok($"Professor with id: {id} successfully deleted.");
  }
}
