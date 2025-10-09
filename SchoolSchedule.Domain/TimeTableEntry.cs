using System.Reflection.Metadata.Ecma335;

namespace SchoolSchedule.Domain
{
    public class TimeTableEntry
    {
        public int TimeTableEntryId { get; set; }
        public Guid TimeTableEntryGuid { get; set; }
        public int? TeacherAssignmentId { get; set; }
        public TeacherAssignment? TeacherAssignment { get; set; }
        public int? SubjectAssignmentId { get; set; }
        public SubjectAssignment SubjectAssignment { get; set; }
        public int DayId { get; set; }
        public SchoolWeek Day { get; set; }
        public int? Period { get; set; }
        public bool IsPlaceHolder => TeacherAssignment?.Teacher?.TeacherName == "س"; 
    }
}
