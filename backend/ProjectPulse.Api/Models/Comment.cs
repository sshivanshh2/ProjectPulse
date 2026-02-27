namespace ProjectPulse.Api.Models;

public class Comment
{
    public int Id { get; set; }
    public string Body { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int TaskItemId { get; set; }
    public TaskItem TaskItem { get; set; } = null!;

    public string AuthorId { get; set; } = string.Empty;
    public AppUser Author { get; set; } = null!;
}