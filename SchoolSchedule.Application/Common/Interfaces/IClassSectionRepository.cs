using SchoolSchedule.Application.DTOs;
using SchoolSchedule.Domain;

namespace SchoolSchedule.Application.Common.Interfaces
{
    public interface IClassSectionRepository
    {
        Task<List<ClassSection>> GetClassSectionsAsync(int? id = null);
        Task<ClassSection> ThisClassSectionIsExist(CreateClassSectionDto createClassSection);
        Task AddAsync(ClassSection classSection);
        Task RemoveAsync(ClassSection classSection);
    }
}
