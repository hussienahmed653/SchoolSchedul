using SchoolSchedule.Application.Authentications.Common;
using SchoolSchedule.Application.DTOs.Authentications;
using SchoolSchedule.Domain;

namespace SchoolSchedule.Application.Mapping.Users
{
    public static class UsersMapper
    {
        public static User MapToUser(this RegisterRequestDto registerRequest)
        {
            return new User
            {
                UserName = registerRequest.FirstName + ' ' + registerRequest.LastName,
                Email = registerRequest.Email,
            };
        }

        public static AuthResult MapToAuthResult(this User user)
        {
            return new AuthResult
            {
                UserName = user.UserName,
                Email = user.Email,
            };
        }
    }
}
