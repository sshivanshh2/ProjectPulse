using Microsoft.AspNetCore.Identity;

namespace ProjectPulse.Api.Models;

public class AppUser : IdentityUser
{
    public string DisplayName { get; set; } = string.Empty;
    public ICollection<Board> Boards { get; set; } = new List<Board>();
}