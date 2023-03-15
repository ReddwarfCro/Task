using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BusinessLayer.Models.Authorization;
using ConfigurationManager = Task.Common.ConfigurationManager;
using Task.Services;

namespace Task.Controllers
{
    /// <summary>
    /// Authentication Controller
    /// </summary>
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [ApiController]
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        /// <summary>
        /// User service
        /// </summary>
        private IUserService _userService { get; set; }

        /// <summary>
        /// Logger Service
        /// </summary>
        private readonly ILogger<HotelController> _logger;

        /// <summary>
        /// Authentication controller constructor
        /// </summary>
        /// <param name="logger">Injected logger</param>
        /// <param name="userService">Injected userService</param>
        public AuthenticationController(
            ILogger<HotelController> logger,
            IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="user">User</param>
        /// <returns>Authentication Token</returns>
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel user)
        {
            if (user is null)
            {
                return BadRequest("Invalid user request!!!");
            }
            var response = _userService.Login(user.UserName, user.Password);

            if (response.Success)
            {
                return Ok(response.Data);
            }

            return Unauthorized();
        }
    }
}