using AutoMapper;
using ProjectPulse.Application.Common.Interfaces;
using ProjectPulse.Application.DTOs.Project;
using ProjectPulse.Domain.Entities;

namespace ProjectPulse.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ProjectService(
            IProjectRepository projectRepository,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProjectDto>> GetAllProjectsAsync()
        {
            var projects = await _projectRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProjectDto>>(projects);
        }

        public async Task<ProjectDto?> GetProjectByIdAsync(int id)
        {
            var project = await _projectRepository.GetByIdWithDetailsAsync(id);

            if (project == null)
                return null;

            return _mapper.Map<ProjectDto>(project);
        }

        public async Task<IEnumerable<ProjectDto>> GetUserProjectsAsync(int userId)
        {
            var projects = await _projectRepository.GetUserProjectsAsync(userId);
            return _mapper.Map<IEnumerable<ProjectDto>>(projects);
        }

        public async Task<ProjectDto> CreateProjectAsync(CreateProjectRequest request)
        {
            var project = _mapper.Map<Project>(request);
            var createdProject = await _projectRepository.CreateAsync(project);

            return _mapper.Map<ProjectDto>(createdProject);
        }

        public async Task<ProjectDto> UpdateProjectAsync(int id, UpdateProjectRequest request)
        {
            var existingProject = await _projectRepository.GetByIdAsync(id);

            if (existingProject == null)
            {
                throw new KeyNotFoundException($"Project with ID {id} not found");
            }

            // Map updated values to existing entity
            _mapper.Map(request, existingProject);

            await _projectRepository.UpdateAsync(existingProject);

            return _mapper.Map<ProjectDto>(existingProject);
        }

        public async Task DeleteProjectAsync(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);

            if (project == null)
            {
                throw new KeyNotFoundException($"Project with ID {id} not found");
            }

            await _projectRepository.DeleteAsync(project);
        }

        public async Task AssignUserToProjectAsync(int projectId, int userId)
        {
            // Validate project exists
            if (!await _projectRepository.ExistsAsync(projectId))
            {
                throw new KeyNotFoundException($"Project with ID {projectId} not found");
            }

            // Validate user exists
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found");
            }

            // Check if already assigned
            if (await _projectRepository.IsUserAssignedAsync(projectId, userId))
            {
                throw new InvalidOperationException("User is already assigned to this project");
            }

            await _projectRepository.AssignUserAsync(projectId, userId);
        }

        public async Task UnassignUserFromProjectAsync(int projectId, int userId)
        {
            // Validate project exists
            if (!await _projectRepository.ExistsAsync(projectId))
            {
                throw new KeyNotFoundException($"Project with ID {projectId} not found");
            }

            // Check if user is assigned
            if (!await _projectRepository.IsUserAssignedAsync(projectId, userId))
            {
                throw new InvalidOperationException("User is not assigned to this project");
            }

            await _projectRepository.UnassignUserAsync(projectId, userId);
        }
    }
}