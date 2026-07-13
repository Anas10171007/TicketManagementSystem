using FIRSTPROJECT.Application.Authentication.DTOs;

namespace FIRSTPROJECT.Application.Authentication.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterDto dto);

    Task<AuthResponseDto> LoginAsync(LoginDto dto);
}