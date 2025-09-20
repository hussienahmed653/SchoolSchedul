using SchoolSchedule.Application.DTOs;
using SchoolSchedule.Domain;

namespace SchoolSchedule.Application.Mapping.JobTitles
{
    public static class JobTitleMapper
    {
        public static List<JobTitlesResponseDto> MapJobTitle(this List<JobTitle> jobTitles)
        {
            return jobTitles.Select(jt => new JobTitlesResponseDto
            {
               JobTitleId = jt.JobTitleId,
               JobTitleName = jt.JobTitleName
            }).ToList();
        }
    }
}
