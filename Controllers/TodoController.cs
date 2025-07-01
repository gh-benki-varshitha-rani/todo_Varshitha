using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;
        }

        // 🔍 GET: /api/Todo — Get todos for logged-in user
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodos()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Unauthorized("User ID not found in token");

            var todos = await _context.TodoItems
                .Where(todo => todo.UserId == userId)
                .ToListAsync();

            return Ok(todos);
        }

        // ➕ POST: /api/Todo — Add a todo for logged-in user
        [HttpPost]
        public async Task<ActionResult<TodoItem>> CreateTodo(TodoItem todoItem)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Unauthorized("User ID not found in token");

            todoItem.UserId = userId;
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodos), new { id = todoItem.Id }, todoItem);
        }

        // ✏️ PUT: /api/Todo/{id} — Update a todo (optional, if needed)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo(long id, TodoItem updatedTodo)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Unauthorized("User ID not found in token");

            var todo = await _context.TodoItems.FindAsync(id);

            if (todo == null || todo.UserId != userId)
                return NotFound();

            // Update fields
            todo.Title = updatedTodo.Title;
            todo.Description = updatedTodo.Description;
            todo.DueDate = updatedTodo.DueDate;
            todo.IsComplete = updatedTodo.IsComplete;
            todo.Priority = updatedTodo.Priority;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // ❌ DELETE: /api/Todo/{id} — Delete a todo (optional, if needed)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(long id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Unauthorized("User ID not found in token");

            var todo = await _context.TodoItems.FindAsync(id);

            if (todo == null || todo.UserId != userId)
                return NotFound();

            _context.TodoItems.Remove(todo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
