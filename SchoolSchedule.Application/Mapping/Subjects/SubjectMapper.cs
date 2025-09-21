using SchoolSchedule.Application.DTOs;
using SchoolSchedule.Domain;

namespace SchoolSchedule.Application.Mapping.Subjects
{
    public static class SubjectMapper
    {
        public static List<SubjectsResponseDto> MapToSubjectsResponseDto(this List<Subject> subject)
        {
            return subject.Select(subject => new SubjectsResponseDto
            {
                SubjectId = subject.SubjectId,
                SubjectName = subject.SubjectName
            }).ToList();
        }

        public static Subject MapToSubject(this string subjectname)
        {
            return new Subject
            {
                SubjectName = subjectname,
            };
        }
    }
}
