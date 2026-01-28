using ProjectPulse.Domain.Entities;
namespace ProjectPulse.Application.Common.Interfaces
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetAllAsync();
        Task<Project?> GetByIdAsync(int id);
        Task<Project?> GetByIdWithDetailsAsync(int id);
        Task<Project> CreateAsync(Project project);
        Task UpdateAsync(Project project);
        Task DeleteAsync(Project project);
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<Project>> GetUserProjectsAsync(int userId);
        Task AssignUserAsync(int projectId, int userId);
        Task UnassignUserAsync(int projectId, int userId);
        Task<bool> IsUserAssignedAsync(int projectId, int userId);
    }
}