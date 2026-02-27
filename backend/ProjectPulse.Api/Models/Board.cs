namespace ProjectPulse.Api.Models;

public class Board
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public string OwnerId { get; set; } = string.Empty;
    public AppUser Owner { get; set; } = null!;
    public ICollection<Column> Columns { get; set; } = new List<Column>();
}