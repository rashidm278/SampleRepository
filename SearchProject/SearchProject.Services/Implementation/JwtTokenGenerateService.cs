using SearchProject.Services.Interfaces;
using System.Security.Claims;
using System.Text;

namespace SearchProject.Services.Implementation
{
    public interface JwtTokenGenerateService : IJwtTokenGenerateService
    {
        private readonly IConfiguration _config;

        public JwtTokenGeneration(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateJwt(string userId, string username, string role)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim("UserId", user.UserId.ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Jwt:ExpiresInMinutes"])),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
