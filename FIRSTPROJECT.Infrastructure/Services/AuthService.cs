using FIRSTPROJECT.Application.Authentication.DTOs;
using FIRSTPROJECT.Application.Authentication.Interfaces;
using FIRSTPROJECT.Application.Common;
using FIRSTPROJECT.Domain.Constants;
using FIRSTPROJECT.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace FIRSTPROJECT.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    public async Task<ServiceResult<AuthResponseDto>> RegisterAsync(RegisterDto registerDto)
    {
        var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);

        if (existingUser != null)
        {
            return ServiceResult<AuthResponseDto>.Fail(HttpStatusCode.Conflict, ResponseMessages.EmailAlreadyExists);
        }

        var user = new ApplicationUser
        {
            FullName = registerDto.FullName,
            UserName = registerDto.Email,
            Email = registerDto.Email
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return ServiceResult<AuthResponseDto>.Fail(HttpStatusCode.BadRequest, errors);
        }

        var response = new AuthResponseDto
        {
            Email = user.Email!,
            FullName = user.FullName,
            Token = GenerateJwtToken(user)
        };

        return ServiceResult<AuthResponseDto>.Ok(response);
    }

    public async Task<ServiceResult<AuthResponseDto>> LoginAsync(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);

        if (user == null)
        {
            return ServiceResult<AuthResponseDto>.Fail(HttpStatusCode.Unauthorized, ResponseMessages.InvalidCredentials);
        }

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);

        if (!isPasswordValid)
        {
            return ServiceResult<AuthResponseDto>.Fail(HttpStatusCode.Unauthorized, ResponseMessages.InvalidCredentials);
        }

        var response = new AuthResponseDto
        {
            Email = user.Email!,
            FullName = user.FullName,
            Token = GenerateJwtToken(user)
        };

        return ServiceResult<AuthResponseDto>.Ok(response);
    }

    private string GenerateJwtToken(ApplicationUser user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.FullName)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpiryMinutes"])),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}