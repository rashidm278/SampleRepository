using Microsoft.AspNetCore.Identity;

namespace SearchProject.Api.Utility
{
    public static class PasswordHasherHelper
    {
        private static readonly PasswordHasher<string> _hasher = new();

        public static string HashPassword(string userName, string password)
        {
            return _hasher.HashPassword(userName, password);
        }

        public static bool VerifyPassword(string userName, string password, string hashedPassword)
        {
            var result = _hasher.VerifyHashedPassword(userName, hashedPassword, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}
