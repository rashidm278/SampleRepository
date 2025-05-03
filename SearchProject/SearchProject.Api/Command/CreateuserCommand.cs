using MediatR;
using SearchProject.Entities;

namespace SearchProject.Api.Command
{
    public class CreateUserCommand : IRequest<User>
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int Role { get; set; }

    }
}
