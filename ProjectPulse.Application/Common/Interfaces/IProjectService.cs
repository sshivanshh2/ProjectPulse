using ProjectPulse.Application.DTOs.Project;
namespace ProjectPulse.Application.Common.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDto>> GetAllProjectsAsync();
        Task<ProjectDto?> GetProjectByIdAsync(int id);
        Task<IEnumerable<ProjectDto>> GetUserProjectsAsync(int userId);
        Task<ProjectDto> CreateProjectAsync(CreateProjectRequest request);
        Task<ProjectDto> UpdateProjectAsync(int id, UpdateProjectRequest request);
        Task DeleteProjectAsync(int id);
        Task AssignUserToProjectAsync(int projectId, int userId);
        Task UnassignUserFromProjectAsync(int projectId, int userId);
    }
}