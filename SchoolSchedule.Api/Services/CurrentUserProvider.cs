using Microsoft.AspNetCore.Mvc;
using SchoolSchedule.Application.Common.Interfaces;
using SchoolSchedule.Application.Common.Models;
using System.Security.Claims;

namespace SchoolSchedule.Api.Services
{
    public class CurrentUserProvider(IHttpContextAccessor _httpContextAccessor) : ICurrentUserProvider
    {
        public CurrentUser GetCurrentUser()
        {
            var UserId = GetClaims("UserID")
                .Select(int.Parse)
                .First();
            var Email = GetClaims(ClaimTypes.Email)
                .First();
            var Roles = GetClaims(ClaimTypes.Role)
                .ToList();
            return new CurrentUser(UserId, Email, Roles);
        }
        private IReadOnlyList<string> GetClaims(string type)
        {
            return _httpContextAccessor.HttpContext.User.Claims
                .Where(c => c.Type == type)
                .Select(c => c.Value)
                .ToList();
        }
    }
}
