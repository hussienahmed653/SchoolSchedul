using SchoolSchedule.Domain;

namespace SchoolSchedule.Application.Common.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> ExistByEmailAsync(string email);
        Task AddAsync(User user);
        Task<User> GetUser(string email);
    }
}
