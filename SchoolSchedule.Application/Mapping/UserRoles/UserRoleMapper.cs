using SchoolSchedule.Domain;

namespace SchoolSchedule.Application.Mapping.UserRoles
{
    public static class UserRoleMapper
    {
        public static UserRole MappToUserRoles(this int userid, int roleid)
        {
            return new UserRole
            {
                UserId = userid,
                RoleId = roleid
            };
        }
    }
}
