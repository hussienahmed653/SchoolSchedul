using ErrorOr;
using SchoolSchedule.Domain.Common.Interfaces;
using System.Text.RegularExpressions;

namespace SchoolSchedule.Infrastructure.Authentication.PasswordHasher
{
    internal partial class PasswordHasher : IPasswordHasher
    {
        private static readonly Regex _strongPasswordRegex = StrongPasswordRegex();
        public ErrorOr<string> HashPassword(string password)
        {
            return _strongPasswordRegex.IsMatch(password)
                ? BCrypt.Net.BCrypt.HashPassword(password)
                : Error.Conflict("Password.NotStrong", "Password is not strong enough. It must be at least 10 characters long, contain at least one uppercase letter, one lowercase letter, and one digit, and have no whitespace.");
        }

        public bool VerifyPassword(string password, string hashpassword)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(password, hashpassword);
        }
        [GeneratedRegex("^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])\\S{10,}$")]
        private static partial Regex StrongPasswordRegex();
    }
}
