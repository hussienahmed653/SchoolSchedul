using SchoolSchedule.Application.Authentications.Common;
using SchoolSchedule.Domain;

namespace SchoolSchedule.Application.Mapping.Roles
{
    public static class RolesMapper
    {
        public static RolesResponse GetAllRolesMapper(this List<Role> roles)
        {
            return new RolesResponse
            {
                Roles = roles
                            .Select(r => r.RoleName)
                            .ToList()
            };
        }
    }
}
