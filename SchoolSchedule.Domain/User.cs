namespace SchoolSchedule.Domain
{
    public class User
    {
        public int UserId { get; set; }
        public Guid UserGuid { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
