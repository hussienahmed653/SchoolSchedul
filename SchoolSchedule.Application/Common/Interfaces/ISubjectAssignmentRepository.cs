using SchoolSchedule.Domain;

namespace SchoolSchedule.Application.Common.Interfaces
{
    public interface ISubjectAssignmentRepository
    {
        Task AddAsync(SubjectAssignment subjectAssignment);
        Task<List<SubjectAssignment>> GetAllAsync(int pagenumber);
    }
}
