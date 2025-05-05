using MediatR;
using Microsoft.IdentityModel.Tokens;
using SearchProject.Command;
using SearchProject.Domain.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using SearchProject.Application.Dtos;
using SearchProject.Domain.Entities;

namespace SearchProject.Application.Handlers
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthResponse>
    {
        private readonly AuthSettings _authSettings;

        private readonly IUserRepository _userRepository;

        public RefreshTokenCommandHandler(IUserRepository userRepository, IOptions<AuthSettings> options)
        {
            _userRepository = userRepository;
           _authSettings = options.Value;
        }

        public async Task<AuthResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUsernameAsync(request.Username);
            if (user == null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiry < DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException("Invalid or expired refresh token");
            }

            var newToken = GenerateJwt(user);
            var newRefreshToken = Guid.NewGuid().ToString();

            await _userRepository.UpdateRefreshTokenAsync(user, newRefreshToken, DateTime.UtcNow.AddDays(7));

            return new AuthResponse() { Token = newToken, RefreshToken = newRefreshToken };
        }

        private string GenerateJwt(User user)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim("UserId", user.UserId.ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _authSettings.Issuer,
                audience: _authSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_authSettings.ExpiresInMinutes)),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
