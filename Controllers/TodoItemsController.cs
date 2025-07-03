using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoService _todoService;

        public TodoItemsController(TodoService todoService)
        {
            _todoService = todoService;
        }

        // 🔍 GET: /api/TodoItems
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Example usage in controller
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdStr, out var userId))
                return Unauthorized();

            // Now use userId (int) in your service calls
            var todos = await _todoService.GetAllAsync(userId);
            return Ok(todos);
        }

        // 🔍 GET: /api/TodoItems/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdStr, out var userId))
                return Unauthorized(); // or handle error

            var todo = await _todoService.GetByIdAsync(id, userId);
            if (todo == null) return NotFound();

            return Ok(todo);
        }

        // ➕ POST: /api/TodoItems
        [HttpPost]
        public async Task<IActionResult> Create(TodoItemDTO dto)
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdStr, out var userId))
                return Unauthorized(); // or handle error

            var createdTodo = await _todoService.CreateAsync(dto, userId);
            return CreatedAtAction(nameof(GetById), new { id = createdTodo.Id }, createdTodo);
        }

        // ✏️ PUT: /api/TodoItems/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, TodoItemDTO dto)
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdStr, out var userId))
                return Unauthorized(); // or handle error

            var updated = await _todoService.UpdateAsync(id, dto, userId);
            if (updated == null) return NotFound();

            return Ok(updated);
        }

        // ❌ DELETE: /api/TodoItems/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdStr, out var userId))
                return Unauthorized(); // or handle error

            var deleted = await _todoService.DeleteAsync(id, userId);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}