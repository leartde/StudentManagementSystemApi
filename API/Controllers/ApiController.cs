using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiVersion("1")]
[ApiController]
// [Route("api/v{version:apiVersion}/[controller]s")]
[Route("api/v1/[controller]s")] //temporary

public class ApiController : ControllerBase
{
}
