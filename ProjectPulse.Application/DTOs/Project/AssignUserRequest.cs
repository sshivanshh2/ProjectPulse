using System.ComponentModel.DataAnnotations;
namespace ProjectPulse.Application.DTOs.Project
{
    public class AssignUserRequest
    {
        [Required(ErrorMessage = "User ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "User ID must be greater than 0")]
        public int UserId { get; set; }
    }
}