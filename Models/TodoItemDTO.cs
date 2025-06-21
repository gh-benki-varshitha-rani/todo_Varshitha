namespace TodoApi.Models;

public class TodoItemDTO
{
    public long Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsComplete { get; set; }
    public int Priority { get; set; }
}
