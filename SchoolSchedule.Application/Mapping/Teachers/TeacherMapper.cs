using SchoolSchedule.Application.DTOs;
using SchoolSchedule.Domain;

namespace SchoolSchedule.Application.Mapping.Teachers
{
    public static class TeacherMapper
    {
        public static Teacher MapToTeacher(this CreateTeacherDto createTeacherDtos)
        {
            return new Teacher
            {
                TeacherName = createTeacherDtos.TeacherName,
                BirthDate = createTeacherDtos.BirthDate,
                JobTitleId = createTeacherDtos.JobTitleId,
                HireDate = createTeacherDtos.HireDate,
                MinistryStartDate = createTeacherDtos.MinistryStartDate,
                SchoolStartDate = createTeacherDtos.SchoolStartDate,
                WorkType = createTeacherDtos.WorkType,
                Workload = createTeacherDtos.Workload,
                
            };
        }
    }
}
