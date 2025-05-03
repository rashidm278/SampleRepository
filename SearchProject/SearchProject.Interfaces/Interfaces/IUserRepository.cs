using SearchProject.Entities;

namespace SearchProject.Interfaces
{
    /// <summary>
    /// user rpository interface
    /// </summary>
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// get user by username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<User?> GetByUsernameAsync(string username);

        /// <summary>
        /// update refresh token method
        /// </summary>
        /// <param name="user"></param>
        /// <param name="refreshToken"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        Task UpdateRefreshTokenAsync(User user, string refreshToken, DateTime expiry);

    }
}
