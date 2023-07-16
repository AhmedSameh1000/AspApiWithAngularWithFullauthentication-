using JWTApi.Data.Helpers;
using JWTApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TestApiJWT.Models;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace JWTApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;

        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<JWT> jwt)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = jwt.Value;
        }

        public async Task<AuthModel> GetToken(TokenRequestModel model)
        {
            var authModel = new AuthModel();

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authModel.Message = "Email or Password is incorrect!";
                return authModel;
            }

            var jwtSecurityToken = await CreateJwtToken(user);
            var rolesList = await _userManager.GetRolesAsync(user);

            authModel.isAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Email = user.Email;
            authModel.UsreName = user.UserName;
            authModel.Roles = rolesList.ToList();

            if (user.RefreshTokens.Any(t => t.IsActive))
            {
                var ActiveRefreshToken = user.RefreshTokens.FirstOrDefault(t => t.IsActive);
                authModel.RefreshToken = ActiveRefreshToken.Token;
                authModel.RefreshTokenExpiration = ActiveRefreshToken.ExpiresOn;
            }
            else
            {
                var RefreshToken = GenrateRefreshToken();
                authModel.RefreshToken = RefreshToken.Token;
                authModel.RefreshTokenExpiration = RefreshToken.ExpiresOn;
                user.RefreshTokens.Add(RefreshToken);
                await _userManager.UpdateAsync(user);
            }
            return authModel;
        }

        public async Task<AuthModel> RegisterAsync(RegisterModel model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
                return new AuthModel { Message = "Email is Already Regitsered" };

            if (await _userManager.FindByEmailAsync(model.UserName) is not null)
                return new AuthModel { Message = "UserName is Already Regitered" };

            var User = new ApplicationUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };
            var Result = await _userManager.CreateAsync(User, model.Password);

            if (!Result.Succeeded)
            {
                var Error = string.Empty;
                foreach (var error in Result.Errors)
                {
                    Error += $"{error.Description},";
                }
                return new AuthModel { Message = Error };
            }

            await _userManager.AddToRoleAsync(User, "User");
            var Jwt = await CreateJwtToken(User);

            var refreshToken = GenrateRefreshToken();
            User.RefreshTokens?.Add(refreshToken);
            await _userManager.UpdateAsync(User);

            var Model = new AuthModel
            {
                Email = User.Email,
                //ExpireOn = Jwt.ValidTo,
                isAuthenticated = true,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(Jwt),
                UsreName = User.UserName,
                RefreshToken = refreshToken.Token,
                RefreshTokenExpiration = refreshToken.ExpiresOn,
            };
            return Model;
        }

        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        private RefreshToken GenrateRefreshToken()
        {
            var RandomNumber = new byte[32];
            using var Genrator = new RNGCryptoServiceProvider();
            Genrator.GetBytes(RandomNumber);
            return new RefreshToken()
            {
                Token = Convert.ToBase64String(RandomNumber),
                ExpiresOn = DateTime.UtcNow.AddDays(10),
                CreatedOn = DateTime.UtcNow
            };
        }

        public async Task<AuthModel> RefreshTokenAsync(string Token)
        {
            var authmodel = new AuthModel();

            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == Token));

            if (user == null)
            {
                authmodel.Message = "Invalid Token";
                return authmodel;
            }

            var refrehtoken = user.RefreshTokens.Single(t => t.Token == Token);
            if (!refrehtoken.IsActive)
            {
                authmodel.Message = "InActive Token";
                return authmodel;
            }
            refrehtoken.RevokeOn = DateTime.UtcNow;
            var NewRefreshToken = GenrateRefreshToken();
            user.RefreshTokens.Add(NewRefreshToken);
            await _userManager.UpdateAsync(user);
            var JwtToken = await CreateJwtToken(user);

            authmodel.isAuthenticated = true;
            authmodel.Token = new JwtSecurityTokenHandler().WriteToken(JwtToken);
            authmodel.Email = user.Email;
            authmodel.UsreName = user.UserName;
            var Roles = await _userManager.GetRolesAsync(user);
            authmodel.Roles = Roles.ToList();
            authmodel.RefreshToken = NewRefreshToken.Token;
            authmodel.RefreshTokenExpiration = NewRefreshToken.ExpiresOn;
            return authmodel;
        }

        public async Task<bool> RevokeTokenAsync(string Token)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == Token));

            if (user == null)
                return false;

            var refrehtoken = user.RefreshTokens.Single(t => t.Token == Token);
            if (!refrehtoken.IsActive)
                return false;

            refrehtoken.RevokeOn = DateTime.UtcNow;

            await _userManager.UpdateAsync(user);
            return true;
        }
    }
}