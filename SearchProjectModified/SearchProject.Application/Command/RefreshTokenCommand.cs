using MediatR;

namespace SearchProject.Command
{
    public class RefreshTokenCommand : IRequest<AuthResponse>
    {
        public string Username { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;

    }
}
