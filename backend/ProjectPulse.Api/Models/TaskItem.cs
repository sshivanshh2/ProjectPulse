namespace ProjectPulse.Api.Models;

public enum Priority { Low, Medium, High }

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Priority Priority { get; set; } = Priority.Medium;
    public DateTime? DueDate { get; set; }
    public int Order { get; set; }
    public bool IsArchived { get; set; } = false;

    public int ColumnId { get; set; }
    public Column Column { get; set; } = null!;

    public string? AssigneeId { get; set; }
    public AppUser? Assignee { get; set; }
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}