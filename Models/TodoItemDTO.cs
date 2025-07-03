namespace TodoApi.Models;

public class TodoItemDTO
{
    public long Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsComplete { get; set; }
    public int Priority { get; set; } // Changed from string to int for consistency

    // For admin/task assignment support
    public int OwnerId { get; set; }
    public int? AssignedToId { get; set; }
    public string? OwnerName { get; set; }
    public string? AssignedToName { get; set; }
}
