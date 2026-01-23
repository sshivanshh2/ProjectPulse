using ProjectPulse.Domain.Common;

namespace ProjectPulse.Domain.Entities
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; } = string.Empty;

        // Foreign keys
        public int TaskId { get; set; }
        public ProjectTask Task { get; set; } = null!;

        public int AuthorId { get; set; }
        public User Author { get; set; } = null!;
    }
}