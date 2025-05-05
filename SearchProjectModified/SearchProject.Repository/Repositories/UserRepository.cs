using Microsoft.EntityFrameworkCore;
using SearchProject.Domain.Entities;
using SearchProject.Domain.Interfaces;
using SearchProject.Repository.Data;

namespace SearchProject.Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="context"></param>
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<User> AddAsync(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> GetByUsernameAsync(string userName)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Username == userName);
        }

        public async Task UpdateRefreshTokenAsync(User user, string refreshToken, DateTime expiry)
        {
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = expiry;
            await _context.SaveChangesAsync();
        }
    }
}
