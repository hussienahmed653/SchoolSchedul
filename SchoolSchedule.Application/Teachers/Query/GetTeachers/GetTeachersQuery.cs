using ErrorOr;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.DTOs;
using SchoolSchedule.Domain;

namespace SchoolSchedule.Application.Teachers.Query.GetTeachers
{
    public record GetTeachersQuery(string? teachername = null) : IRequest<ErrorOr<List<TeacherResponseDto>>>;
}
