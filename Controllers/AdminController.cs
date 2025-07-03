using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly TodoContext _context;

        public AdminController(TodoContext context)
        {
            _context = context;
        }

        // ✅ Get all users (for dropdown or management)
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _context.Users
                .Select(u => new { u.Id, u.Username, u.Role })
                .ToListAsync();

            return Ok(users);
        }

        // ✅ Create a new user with a role
        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
        {
            if (await _context.Users.AnyAsync(u => u.Username == dto.Username))
                return BadRequest("User already exists.");

            var user = new User
            {
                Username = dto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = dto.Role
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User created successfully." });
        }

        // ✅ Get all tasks across all users (admin only)
        [HttpGet("tasks")]
        public async Task<IActionResult> GetAllTasks()
        {
            var tasks = await _context.TodoItems
                .Include(t => t.Owner)
                .Include(t => t.AssignedTo)
                .Select(t => new TodoItemDTO
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    DueDate = t.DueDate,
                    IsComplete = t.IsComplete,
                    Priority = t.Priority,
                    OwnerId = t.OwnerId,
                    AssignedToId = t.AssignedToId,
                    OwnerName = t.Owner != null ? t.Owner.Username : "N/A",
                    AssignedToName = t.AssignedTo != null ? t.AssignedTo.Username : "N/A"
                })
                .ToListAsync();

            return Ok(tasks);
        }
    }

    // DTO for creating a user with a role
    public class CreateUserDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // "Admin" or "User"
    }
}

