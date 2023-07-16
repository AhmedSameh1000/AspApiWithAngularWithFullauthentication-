using System.Text.Json.Serialization;

namespace JWTApi.Models
{
    public class AuthModel
    {
        public string Message { get; set; }
        public bool isAuthenticated { set; get; }
        public string UsreName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public string Token { get; set; }

        //public DateTime ExpireOn { get; set; }
        [JsonIgnore]
        public string? RefreshToken { get; set; }

        public DateTime RefreshTokenExpiration { get; set; }
    }
}