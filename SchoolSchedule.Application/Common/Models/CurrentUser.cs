namespace SchoolSchedule.Application.Common.Models
{
    public record CurrentUser(int UserId, string Email, List<string> Roles);
}
