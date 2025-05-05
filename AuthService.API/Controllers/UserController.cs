using AuthService.Application.DTOs;
using AuthService.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            _logger.LogInformation("Register Called with request: {@request}", request);

            try
            {
                var response = await _userService.RegisterAsync(request);
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginRequest request)
        {
            _logger.LogInformation("Login Called with request: {@request}", request);
            try
            {
                var result = await _userService.LoginAsync(request);
                return Ok(new { Success = result });
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning("Unauthorized access: {Message}", ex.Message);
                return Unauthorized(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred: {Message}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            _logger.LogInformation("Logout Called");
            //_userService.Logout();
            return Ok();
        }
    }
}
