using ProjectPulse.Domain.Common;
using TaskStatus = ProjectPulse.Domain.Enums.TaskStatus;  // ← Alias to resolve conflict
using ProjectPulse.Domain.Enums;

namespace ProjectPulse.Domain.Entities
{
    public class ProjectTask : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TaskStatus Status { get; set; } = TaskStatus.ToDo;
        public TaskPriority Priority { get; set; } = TaskPriority.Medium;
        public DateTime? DueDate { get; set; }

        // Foreign keys
        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!;

        public int? AssignedToId { get; set; }
        public User? AssignedTo { get; set; }

        // Navigation properties
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}