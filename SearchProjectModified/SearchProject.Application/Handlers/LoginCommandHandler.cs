using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SearchProject.Application.Utility;
using SearchProject.Command;
using SearchProject.Domain.Entities;
using SearchProject.Application.Dtos;
using SearchProject.Domain.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SearchProject.Application.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse>
    {
        private readonly AuthSettings _authSettings;
        private readonly IUserRepository _userRepository;


        public LoginCommandHandler(IUserRepository userRepository, IOptions<AuthSettings> options)
        {
            _userRepository = userRepository;
           _authSettings = options.Value;
        }

        public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUsernameAsync(request.Username);
            if (user == null || !PasswordHasherHelper.VerifyPassword(request.Username, request.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid credentials");

            var token = GenerateJwt(user);
            var refreshToken = Guid.NewGuid().ToString();

            await _userRepository.UpdateRefreshTokenAsync(user, refreshToken, DateTime.UtcNow.AddDays(7));

            return new AuthResponse() { Token = token, RefreshToken = refreshToken};
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
