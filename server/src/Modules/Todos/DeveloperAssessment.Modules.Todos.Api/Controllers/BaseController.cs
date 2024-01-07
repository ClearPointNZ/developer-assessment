using Microsoft.AspNetCore.Mvc;

namespace DeveloperAssessment.Modules.Todos.Api.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
[Consumes("application/json")]
internal abstract class BaseController : ControllerBase;
