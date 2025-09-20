namespace SchoolSchedule.Domain
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public Guid TeacherGuid { get; set; }
        public string TeacherName { get; set; }
        public int JobTitleId { get; set; }
        public JobTitle JobTitle { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime HireDate { get; set; } // تاريخ التعيين
        public DateTime MinistryStartDate { get; set; } // تاريخ بداية الخدمة في وزارة التربية
        public DateTime SchoolStartDate { get; set; } // تاريخ بداية الخدمة في المدرسة
        public WorkType WorkType { get; set; } // كلي / جزئي
        public int Workload { get; set; } // النصاب
        public DateTime AddedOn { get; set; }
        public bool IsActive { get; set; }

    }

    public enum WorkType
    {
        كلي = 1,
        جزئي = 2
    }
}
