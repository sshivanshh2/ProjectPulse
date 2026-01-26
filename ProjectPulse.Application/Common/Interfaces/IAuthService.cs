using ProjectPulse.Application.DTOs.Auth;

namespace ProjectPulse.Application.Common.Interfaces
{
    // Register and Login.
    // Returns AuthResponse (which includes the JWT token).
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(RegisterRequest request);
        Task<AuthResponse> LoginAsync(LoginRequest request);
    }
}
