using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Services
{
    public class TodoService
    {
        private readonly TodoContext _context;

        public TodoService(TodoContext context)
        {
            _context = context;
        }

        private TodoItemDTO ToDTO(TodoItem item) => new TodoItemDTO
        {
            Id = item.Id,
            Title = item.Title,
            Description = item.Description,
            DueDate = item.DueDate,
            IsComplete = item.IsComplete,
            Priority = item.Priority
        };

        // 🔍 Get all todos for a specific user
        public async Task<IEnumerable<TodoItemDTO>> GetAllAsync(string userId)
        {
            return await _context.TodoItems
                .Where(t => t.UserId == userId)
                .Select(t => ToDTO(t))
                .ToListAsync();
        }

        // 🔍 Get a single todo (if owned by user)
        public async Task<TodoItemDTO?> GetByIdAsync(long id, string userId)
        {
            var todo = await _context.TodoItems
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

            return todo == null ? null : ToDTO(todo);
        }

        // ➕ Create new todo for a specific user
        public async Task<TodoItemDTO> CreateAsync(TodoItemDTO dto, string userId)
        {
            var todo = new TodoItem
            {
                Title = dto.Title,
                Description = dto.Description,
                DueDate = dto.DueDate,
                IsComplete = dto.IsComplete,
                Priority = dto.Priority,
                UserId = userId
            };

            _context.TodoItems.Add(todo);
            await _context.SaveChangesAsync();

            return ToDTO(todo);
        }

        // ✏️ Update todo (only if it belongs to the user)
        public async Task<TodoItemDTO?> UpdateAsync(long id, TodoItemDTO dto, string userId)
        {
            if (id != dto.Id)
                return null;

            var todo = await _context.TodoItems
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

            if (todo == null)
                return null;

            todo.Title = dto.Title;
            todo.Description = dto.Description;
            todo.DueDate = dto.DueDate;
            todo.IsComplete = dto.IsComplete;
            todo.Priority = dto.Priority;

            await _context.SaveChangesAsync();

            return ToDTO(todo);
        }

        // ❌ Delete todo (only if it belongs to the user)
        public async Task<bool> DeleteAsync(long id, string userId)
        {
            var todo = await _context.TodoItems
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

            if (todo == null)
                return false;

            _context.TodoItems.Remove(todo);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}


