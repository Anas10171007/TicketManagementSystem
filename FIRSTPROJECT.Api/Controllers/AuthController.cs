using FIRSTPROJECT.Api.Common;
using FIRSTPROJECT.Application.Authentication.DTOs;
using FIRSTPROJECT.Application.Authentication.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace FIRSTPROJECT.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IValidator<RegisterDto> _registerValidator;
    private readonly IValidator<LoginDto> _loginValidator;

    public AuthController(
        IAuthService authService,
        IValidator<RegisterDto> registerValidator,
        IValidator<LoginDto> loginValidator)
    {
        _authService = authService;
        _registerValidator = registerValidator;
        _loginValidator = loginValidator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var validationResult = await _registerValidator.ValidateAsync(dto);

        if (!validationResult.IsValid)
        {
            return BadRequest(new
            {
                Errors = validationResult.Errors.Select(error => error.ErrorMessage)
            });
        }

        var result = await _authService.RegisterAsync(dto);

        return result.ToActionResult();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var validationResult = await _loginValidator.ValidateAsync(dto);

        if (!validationResult.IsValid)
        {
            return BadRequest(new
            {
                Errors = validationResult.Errors.Select(error => error.ErrorMessage)
            });
        }

        var result = await _authService.LoginAsync(dto);

        return result.ToActionResult();
    }
}