using ErrorOr;

namespace SchoolSchedule.Domain.Common.Interfaces
{
    public interface IPasswordHasher
    {
        public ErrorOr<string> HashPassword(string password);
        public bool VerifyPassword(string password, string hashpassword);
    }
}
