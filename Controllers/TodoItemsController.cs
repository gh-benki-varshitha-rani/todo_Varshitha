using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoItemsController(TodoContext context)
        {
            _context = context;
        }
        private static TodoItemDTO ItemToDTO(TodoItem todoItem) =>
        new TodoItemDTO
        {
            Id = todoItem.Id,
            Title = todoItem.Title,
            Description = todoItem.Description,
            DueDate = todoItem.DueDate,
            IsComplete = todoItem.IsComplete,
            Priority = todoItem.Priority
        };



        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            return await _context.TodoItems.ToListAsync();
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        // PUT: api/TodoItems/5
        [HttpPut("{id}")]
public async Task<IActionResult> PutTodoItem(long id, TodoItemDTO todoDTO)
{
    if (id != todoDTO.Id)
    {
        return BadRequest();
    }

    var todoItem = await _context.TodoItems.FindAsync(id);
    if (todoItem == null)
    {
        return NotFound();
    }

    // Update all fields from DTO
    todoItem.Title = todoDTO.Title;
    todoItem.Description = todoDTO.Description;
    todoItem.DueDate = todoDTO.DueDate;
    todoItem.IsComplete = todoDTO.IsComplete;
    todoItem.Priority = todoDTO.Priority;

    try
    {
        await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException) when (!TodoItemExists(id))
    {
        return NotFound();
    }

    return Ok(ItemToDTO(todoItem)); // Optional: Return the updated object
}


        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> PostTodoItem(TodoItemDTO todoDTO)
        {
            var todoItem = new TodoItem
            {
                Title = todoDTO.Title,
                Description = todoDTO.Description,
                DueDate = todoDTO.DueDate,
                IsComplete = todoDTO.IsComplete,
                Priority = todoDTO.Priority
            };

            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, ItemToDTO(todoItem));
        }
[HttpDelete("{id}")]
public async Task<IActionResult> DeleteTodoItem(long id)
{
    var todoItem = await _context.TodoItems.FindAsync(id);
    if (todoItem == null)
    {
        return NotFound();
    }

    _context.TodoItems.Remove(todoItem);
    await _context.SaveChangesAsync();

    return NoContent();
}




        private bool TodoItemExists(long id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }
    }
}
