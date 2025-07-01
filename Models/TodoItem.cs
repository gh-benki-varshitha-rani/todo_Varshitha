namespace TodoApi.Models;

public class TodoItem
{
    public long Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }  // optional
    public DateTime DueDate { get; set; }
    public bool IsComplete { get; set; }
    public int Priority { get; set; }  // e.g., 1 (High), 2 (Medium), 3 (Low)
        public string ? UserId { get; set; }  // or Guid or int based on your user model
}


