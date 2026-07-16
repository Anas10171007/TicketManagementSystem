using FIRSTPROJECT.Application.Authentication.DTOs;
using FIRSTPROJECT.Application.Common;

namespace FIRSTPROJECT.Application.Authentication.Interfaces;

public interface IAuthService
{
    Task<ServiceResult<AuthResponseDto>> RegisterAsync(RegisterDto registerDto);
    Task<ServiceResult<AuthResponseDto>> LoginAsync(LoginDto loginDto);
}