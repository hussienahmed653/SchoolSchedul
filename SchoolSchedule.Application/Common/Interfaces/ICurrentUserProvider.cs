using SchoolSchedule.Application.Common.Models;

namespace SchoolSchedule.Application.Common.Interfaces
{
    public interface ICurrentUserProvider
    {
        CurrentUser GetCurrentUser();
    }
}
