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

        public async Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            return await _context.TodoItems.ToListAsync();
        }

        public async Task<TodoItem?> GetByIdAsync(long id)
        {
            return await _context.TodoItems.FindAsync(id);
        }

        public async Task<TodoItemDTO> CreateAsync(TodoItemDTO dto)
        {
            var todo = new TodoItem
            {
                Title = dto.Title,
                Description = dto.Description,
                DueDate = dto.DueDate,
                IsComplete = dto.IsComplete,
                Priority = dto.Priority
            };

            _context.TodoItems.Add(todo);
            await _context.SaveChangesAsync();
            return ToDTO(todo);
        }

        public async Task<TodoItemDTO?> UpdateAsync(long id, TodoItemDTO dto)
        {
            if (id != dto.Id)
                return null;

            var todo = await _context.TodoItems.FindAsync(id);
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

        public async Task<bool> DeleteAsync(long id)
        {
            var todo = await _context.TodoItems.FindAsync(id);
            if (todo == null)
                return false;

            _context.TodoItems.Remove(todo);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}


