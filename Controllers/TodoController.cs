using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TodoApi.Controllers
{
    [Authorize] // 🔐 Requires valid JWT token
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetTodos()
        {
            var todos = new[]
            {
                new { Id = 1, Title = "Finish .NET app", Done = false },
                new { Id = 2, Title = "Test JWT token", Done = true }
            };

            return Ok(todos);
        }
    }
}
