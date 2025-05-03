using MediatR;
using SearchProject.Api.Command;
using SearchProject.Api.Utility;
using SearchProject.Entities;
using SearchProject.Interfaces;

namespace SearchProject.Api.Handlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
    {
        private readonly IUserRepository _repository;

        public CreateUserCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var hashedPassword = PasswordHasherHelper.HashPassword(request.Username, request.Password);

            var newUser = new User
            {
                Username = request.Username,
                PasswordHash = hashedPassword,
                Email = request.Email,
                Role = (UserRole)request.Role
            };

            await _repository.AddAsync(newUser);
            return newUser;
        }
    }
}
