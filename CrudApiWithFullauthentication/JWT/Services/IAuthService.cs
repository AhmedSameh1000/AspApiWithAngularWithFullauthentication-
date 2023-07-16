using JWTApi.Models;
using TestApiJWT.Models;

namespace JWTApi.Services
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(RegisterModel registerModel);

        Task<AuthModel> GetToken(TokenRequestModel model);

        Task<AuthModel> RefreshTokenAsync(string Token);

        Task<bool> RevokeTokenAsync(string Token);
    }
}