using JWTApi.Models;
using JWTApi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestApiJWT.Models;

namespace JWTApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthController(IAuthService authService, UserManager<ApplicationUser> userManager)
        {
            _authService = authService;
            _userManager = userManager;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel registerModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AuthModel result = await _authService.RegisterAsync(registerModel);
            if (!result.isAuthenticated)
            {
                return BadRequest(result.Message);
            }

            SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

            return Ok(result);
        }

        [HttpPost("GetTokenAsync")]
        public async Task<IActionResult> GetTokenAsync([FromBody] TokenRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AuthModel result = await _authService.GetToken(model);
            if (!result.isAuthenticated)
            {
                return BadRequest(result.Message);
            }

            if (!string.IsNullOrEmpty(result.RefreshToken))
            {
                SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);
            }

            return Ok(result);
        }

        private void SetRefreshTokenInCookie(string RefreshToken, DateTime Expires)
        {
            CookieOptions CookieOption = new()
            {
                HttpOnly = true,
                Expires = Expires.ToLocalTime(),
                Secure = true,
                IsEssential = true,
                SameSite = SameSiteMode.None,
            };
            Response.Cookies.Append("RefreshToken", RefreshToken, CookieOption);
        }

        [HttpGet("RefreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            string? refreshToken = Request.Cookies["RefreshToken"];
            AuthModel result = await _authService.RefreshTokenAsync(refreshToken);
            if (!result.isAuthenticated)
            {
                return BadRequest(result);
            }

            SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);
            return Ok(result);
        }

        [HttpPost("RevokeToken")]
        public async Task<IActionResult> RevokeToken([FromBody] RevokeModel Model)
        {
            string? Token = Model.Token ?? Request.Cookies["RefreshToken"];

            if (string.IsNullOrEmpty(Token))
            {
                return BadRequest("Token Is Required");
            }

            bool result = await _authService.RevokeTokenAsync(Token);

            return !result ? BadRequest("Token Is Invalid") : Ok();
        }

        [HttpGet]
        public IActionResult GetData()
        {
            return Ok(10);
        }

    }
}