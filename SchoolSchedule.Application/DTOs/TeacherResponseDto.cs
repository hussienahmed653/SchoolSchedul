using SchoolSchedule.Domain;

namespace SchoolSchedule.Application.DTOs
{
    public class TeacherResponseDto
    {
        public string TeacherName { get; set; }
        public string JobTitle { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime HireDate { get; set; } // تاريخ التعيين
        public DateTime MinistryStartDate { get; set; } // تاريخ بداية الخدمة في وزارة التربية
        public DateTime SchoolStartDate { get; set; } // تاريخ بداية الخدمة في المدرسة
        public string WorkType { get; set; } // كلي / جزئي
        public int Workload { get; set; } // النصاب
        public DateTime AddedOn { get; set; }
        public List<SubjectDto> Subject { get; set; } = new List<SubjectDto>();
    }
}
