namespace TodoApi.Models;
public class TodoItem
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime DueDate { get; set; }
    public bool IsComplete { get; set; }
    public int Priority { get; set; } // Changed from string to int for consistency

    public int OwnerId { get; set; }
    public User? Owner { get; set; } // Navigation property

    public int? AssignedToId { get; set; }
    public User? AssignedTo { get; set; } // Navigation property
}




