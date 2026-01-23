using ProjectPulse.Domain.Common;

namespace ProjectPulse.Domain.Entities
{
    public class Project : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        // Navigation properties
        public ICollection<ProjectUser> ProjectUsers { get; set; } = new List<ProjectUser>();
        public ICollection<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();
    }
}