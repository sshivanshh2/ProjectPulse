using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectPulse.Application.Common.Interfaces;
using ProjectPulse.Application.DTOs.Project;
using System.Security.Claims;

namespace ProjectPulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // All endpoints require authentication
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        // GET: api/projects
        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            try
            {
                var projects = await _projectService.GetAllProjectsAsync();
                return Ok(projects);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while retrieving projects" });
            }
        }

        // GET: api/projects/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            try
            {
                var project = await _projectService.GetProjectByIdAsync(id);

                if (project == null)
                    return NotFound(new { error = $"Project with ID {id} not found" });

                return Ok(project);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while retrieving the project" });
            }
        }

        // GET: api/projects/user/5
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserProjects(int userId)
        {
            try
            {
                var projects = await _projectService.GetUserProjectsAsync(userId);
                return Ok(projects);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while retrieving user projects" });
            }
        }

        // GET: api/projects/my-projects
        [HttpGet("my-projects")]
        public async Task<IActionResult> GetMyProjects()
        {
            try
            {
                // Get current user ID from JWT claims
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

                if (userIdClaim == null)
                    return Unauthorized();

                int userId = int.Parse(userIdClaim.Value);
                var projects = await _projectService.GetUserProjectsAsync(userId);

                return Ok(projects);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while retrieving your projects" });
            }
        }

        // POST: api/projects
        [HttpPost]
        [Authorize(Roles = "Admin,ProjectManager")] // Only admins and PMs can create
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectRequest request)
        {
            try
            {
                var project = await _projectService.CreateProjectAsync(request);
                return CreatedAtAction(nameof(GetProjectById), new { id = project.Id }, project);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while creating the project" });
            }
        }

        // PUT: api/projects/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,ProjectManager")]
        public async Task<IActionResult> UpdateProject(int id, [FromBody] UpdateProjectRequest request)
        {
            try
            {
                var project = await _projectService.UpdateProjectAsync(id, request);
                return Ok(project);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while updating the project" });
            }
        }

        // DELETE: api/projects/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] // Only admins can delete
        public async Task<IActionResult> DeleteProject(int id)
        {
            try
            {
                await _projectService.DeleteProjectAsync(id);
                return NoContent(); // 204 No Content (success with no body)
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while deleting the project" });
            }
        }

        // POST: api/projects/5/assign
        [HttpPost("{id}/assign")]
        [Authorize(Roles = "Admin,ProjectManager")]
        public async Task<IActionResult> AssignUser(int id, [FromBody] AssignUserRequest request)
        {
            try
            {
                await _projectService.AssignUserToProjectAsync(id, request.UserId);
                return Ok(new { message = "User assigned successfully" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while assigning the user" });
            }
        }

        // DELETE: api/projects/5/unassign/3
        [HttpDelete("{id}/unassign/{userId}")]
        [Authorize(Roles = "Admin,ProjectManager")]
        public async Task<IActionResult> UnassignUser(int id, int userId)
        {
            try
            {
                await _projectService.UnassignUserFromProjectAsync(id, userId);
                return Ok(new { message = "User unassigned successfully" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while unassigning the user" });
            }
        }
    }
}