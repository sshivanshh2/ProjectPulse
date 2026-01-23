using ProjectPulse.Domain.Common;
using ProjectPulse.Domain.Enums;

namespace ProjectPulse.Domain.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public UserRole Role { get; set; }

        // Navigation properties
        public ICollection<ProjectUser> ProjectUsers { get; set; } = new List<ProjectUser>();
        public ICollection<ProjectTask> AssignedTasks { get; set; } = new List<ProjectTask>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}