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
                ? BCrypt.Net.BCrypt.EnhancedHashPassword(password)
                : Error.Conflict("Password.NotStrong", ".كلمة المرور ليست قوية بما يكفي. يجب ألا يقل طولها عن ١٠ أحرف، وأن تحتوي على حرف كبير واحد على الأقل، وحرف صغير واحد، ورقم واحد، وألا تحتوي على مسافات");
        }

        public bool VerifyPassword(string password, string hashpassword)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(password, hashpassword);
        }
        [GeneratedRegex("^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])\\S{10,}$")]
        private static partial Regex StrongPasswordRegex();
    }
}
