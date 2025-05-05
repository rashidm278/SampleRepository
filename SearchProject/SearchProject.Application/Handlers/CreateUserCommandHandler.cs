using MediatR;
using SearchProject.Domain.Command;
using SearchProject.Application.Utility;
using SearchProject.Domain.Entities;
using SearchProject.Domain.Interfaces;

namespace SearchProject.Application.Handlers
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
