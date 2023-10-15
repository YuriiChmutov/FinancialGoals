using System.Security.Claims;
using FinancialGoals.Core.DTOs.User;
using FinancialGoals.Core.Models;
using FinancialGoals.Data.Data;
using FinancialGoals.Data.Repository.AuthService;
using FinancialGoals.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancialGoals.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly FinancialDbContext _context;

    public AuthController(IAuthService authService, FinancialDbContext context)
    {
        _authService = authService;
        _context = context;
    }
    
    /// <summary>
    /// Registers (creates) new user.
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST api/auth/register
    ///     {
    ///         "email": "yurii@example.com",
    ///         "firstName": "Yurii",
    ///         "secondName": "Chmutov",
    ///         "password": "1111",
    ///         "confirmPassword": "1111"
    ///     }
    /// 
    /// </remarks>
    /// <param name="request">User register data</param>
    /// <returns></returns>
    [HttpPost("register")]
    public async Task<ActionResult<ServiceResponse<string>>> PostUser(UserRegister request)
    {
        var user = new User
        {
            Email = request.Email,
            Role = UserRole.User,
            FirstName = request.FirstName,
            SecondName = request.SecondName
            // PhoneNumber = request.PhoneNumber,
            // Gender = request.Gender
        };
          
        var response = await _authService.Register(user, request.Password);

        if (!response.Success) return BadRequest(response);

        return Ok(response);
    }
    
    /// <summary>
    /// Logins existed user.
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST api/auth/login
    ///     {
    ///         "email": "yurii@example.com",
    ///         "password": "1111"
    ///     }
    /// 
    /// </remarks>
    /// <param name="request">Email and password.</param>
    /// <returns></returns>
    [HttpPost("login")]
    public async Task<ActionResult<ServiceResponse<string>>> Login(UserLogin request)
    {
        var response = await _authService.Login(request.Email, request.Password);
            
        if (!response.Success) return BadRequest(response);

        return Ok(response);
    }

    /// <summary>
    /// Updates password for registered user.
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST api/auth/change-password
    ///     {
    ///         "new_password"
    ///     }
    /// 
    /// </remarks>
    /// <param name="newPassword">A string with a new password</param>
    /// <returns></returns>
    [HttpPost("change-password"), Authorize]
    public async Task<ActionResult<ServiceResponse<bool>>> ChangePassword([FromBody] string newPassword)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var response = await _authService.ChangePassword(int.Parse(userId), newPassword);

        return response.Success ? Ok(response) : BadRequest(response);
    }
}