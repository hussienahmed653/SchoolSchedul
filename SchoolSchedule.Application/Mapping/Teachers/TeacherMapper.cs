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

        public static List<TeacherResponseDto> MapToTeacherResponse(this List<Teacher> teachers)
        {
            return teachers.Select(t => new TeacherResponseDto
            {
                TeacherName = t.TeacherName,
                JobTitle = t.JobTitle.JobTitleName,
                BirthDate = t.BirthDate,
                HireDate = t.HireDate,
                MinistryStartDate = t.MinistryStartDate,
                SchoolStartDate = t.SchoolStartDate,
                WorkType = t.WorkType.ToString(),
                Workload = t.Workload,
                AddedOn = t.AddedOn,
                Subject = t.TeacherAssignments
                .GroupBy(ta => ta.Subject.SubjectName)
                .Select(subjectgroup => new SubjectDto
                {
                    SubjectName = subjectgroup.Key,
                    Grade = subjectgroup
                    .GroupBy(g => g.Grade.GradeYear)
                    .Select(gradeGroup => new GradeDto
                    {
                        GradeYear = gradeGroup.Key,
                        ClassSections = gradeGroup.Select(cs => new ClassSectionDto
                        {
                            ClassSectionName = cs.ClassSection.SectionName
                        }).ToList()
                    }).ToList(),
                }).ToList(),
            }).ToList();
        }
    }
}
