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

        private static TodoItemDTO ToDTO(TodoItem item) => new TodoItemDTO
        {
            Id = item.Id,
            Title = item.Title,
            Description = item.Description,
            DueDate = item.DueDate,
            IsComplete = item.IsComplete,
            Priority = item.Priority,
            OwnerId = item.OwnerId,
            AssignedToId = item.AssignedToId,
            OwnerName = item.Owner?.Username,
            AssignedToName = item.AssignedTo?.Username
        };

        // 🔍 Get all todos for a specific user (owned or assigned)
        public async Task<IEnumerable<TodoItemDTO>> GetAllAsync(int userId)
        {
            return await _context.TodoItems
                .Include(t => t.Owner)
                .Include(t => t.AssignedTo)
                .Where(t => t.OwnerId == userId || t.AssignedToId == userId)
                .Select(t => ToDTO(t))
                .ToListAsync();
        }

        // 🔍 Get a single todo (if owned by or assigned to user)
        public async Task<TodoItemDTO?> GetByIdAsync(long id, int userId)
        {
            var todo = await _context.TodoItems
                .Include(t => t.Owner)
                .Include(t => t.AssignedTo)
                .FirstOrDefaultAsync(t => t.Id == id && (t.OwnerId == userId || t.AssignedToId == userId));

            return todo == null ? null : ToDTO(todo);
        }

        // ➕ Create new todo for a specific user (as owner)
        public async Task<TodoItemDTO> CreateAsync(TodoItemDTO dto, int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                throw new Exception("Invalid user.");

            var todoItem = new TodoItem
            {
                Title = dto.Title,
                Description = dto.Description,
                DueDate = dto.DueDate,
                IsComplete = dto.IsComplete,
                Priority = dto.Priority,
            };

            if (user.Role == "User")
            {
                // Users assign tasks only to themselves
                todoItem.OwnerId = userId;
                todoItem.AssignedToId = userId;
            }
            else if (user.Role == "Admin")
            {
                if (dto.AssignedToId == null || dto.AssignedToId == 0)
                    throw new Exception("AssignedToId is required for admin.");

                var assignedUser = await _context.Users.FindAsync(dto.AssignedToId);
                if (assignedUser == null)
                    throw new Exception("Assigned user not found.");

                todoItem.OwnerId = userId;
                todoItem.AssignedToId = assignedUser.Id;
            }

            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            // Make sure to include navigation properties for correct DTO mapping
            await _context.Entry(todoItem).Reference(t => t.Owner).LoadAsync();
            await _context.Entry(todoItem).Reference(t => t.AssignedTo).LoadAsync();

            return ToDTO(todoItem);
        }

public async Task<TodoItemDTO?> UpdateAsync(long id, TodoItemDTO dto, int userId)
{
    var user = await _context.Users.FindAsync(userId);
    if (user == null) return null;

    bool isAdmin = user.Role == "Admin";

    var todo = await _context.TodoItems
        .Include(t => t.Owner)
        .Include(t => t.AssignedTo)
        .FirstOrDefaultAsync(t => t.Id == id && (isAdmin || t.OwnerId == userId || t.AssignedToId == userId));

    if (todo == null)
        return null;

    todo.Title = dto.Title;
    todo.Description = dto.Description;
    todo.DueDate = dto.DueDate;
    todo.IsComplete = dto.IsComplete;
    todo.Priority = dto.Priority;
    todo.AssignedToId = dto.AssignedToId;

    await _context.SaveChangesAsync();

    return ToDTO(todo);
}


        // ❌ Delete todo (only if it belongs to the user as owner)
        public async Task<bool> DeleteAsync(long id, int userId)
        {
            var todo = await _context.TodoItems
                .FirstOrDefaultAsync(t => t.Id == id && t.OwnerId == userId);

            if (todo == null)
                return false;

            _context.TodoItems.Remove(todo);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

