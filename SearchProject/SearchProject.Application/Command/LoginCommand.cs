using MediatR;
using SearchProject.Domain.Entities;

namespace SearchProject.Command
{
    public class LoginCommand : IRequest<AuthResponse>
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;

    }

    public class AuthResponse : IRequest<User>
    {
        public string Token { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;

    }
}
