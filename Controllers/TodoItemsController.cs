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
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            var todos = await _todoService.GetAllAsync(userId);
            return Ok(todos);
        }

        // 🔍 GET: /api/TodoItems/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            var todo = await _todoService.GetByIdAsync(id, userId);
            if (todo == null) return NotFound();

            return Ok(todo);
        }

        // ➕ POST: /api/TodoItems
        [HttpPost]
        public async Task<IActionResult> Create(TodoItemDTO dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            var createdTodo = await _todoService.CreateAsync(dto, userId);
            return CreatedAtAction(nameof(GetById), new { id = createdTodo.Id }, createdTodo);
        }

        // ✏️ PUT: /api/TodoItems/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, TodoItemDTO dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            var updated = await _todoService.UpdateAsync(id, dto, userId);
            if (updated == null) return NotFound();

            return Ok(updated);
        }

        // ❌ DELETE: /api/TodoItems/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            var deleted = await _todoService.DeleteAsync(id, userId);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}