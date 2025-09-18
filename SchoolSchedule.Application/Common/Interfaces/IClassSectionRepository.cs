using SchoolSchedule.Domain;

namespace SchoolSchedule.Application.Common.Interfaces
{
    public interface IClassSectionRepository
    {
        Task<List<ClassSection>> GetClassSectionsAsync(int? id = null);
    }
}
