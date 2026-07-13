using FIRSTPROJECT.Application.Authentication.DTOs;

namespace FIRSTPROJECT.Application.Authentication.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);

    Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
}