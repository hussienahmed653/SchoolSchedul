using SchoolSchedule.Domain;

namespace SchoolSchedule.Application.DTOs
{
    public class CreateTeacherDto
    {
        public string TeacherName { get; set; }
        public int JobTitleId { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime HireDate { get; set; } // تاريخ التعيين
        public DateTime MinistryStartDate { get; set; } // تاريخ بداية الخدمة في وزارة التربية
        public DateTime SchoolStartDate { get; set; } // تاريخ بداية الخدمة في المدرسة
        public WorkType WorkType { get; set; } // كلي / جزئي
        public int Workload { get; set; } // النصاب

    }
}
