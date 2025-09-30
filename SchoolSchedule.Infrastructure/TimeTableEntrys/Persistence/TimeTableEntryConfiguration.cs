using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolSchedule.Domain;

namespace SchoolSchedule.Infrastructure.TimeTableEntrys.Persistence
{
    internal class TimeTableEntryConfiguration : IEntityTypeConfiguration<TimeTableEntry>
    {
        public void Configure(EntityTypeBuilder<TimeTableEntry> builder)
        {
            builder.HasKey(t => t.TimeTableEntryId);

            builder.Property(t => t.TimeTableEntryId)
                .ValueGeneratedNever();

            builder.Property(t => t.TimeTableEntryGuid)
                .HasDefaultValueSql("NEWID()");

            builder.Property(t => t.IsPlaceHolder)
                .HasComputedColumnSql("CASE WHEN [TeacherAssignmentId] IS NULL THEN 1 ELSE 0 END");

            builder.HasOne(t => t.TeacherAssignment)
                .WithMany(t => t.timeTableEntries)
                .HasForeignKey(t => t.TeacherAssignmentId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(t => t.SubjectAssignment)
                .WithMany(s => s.timeTableEntries)
                .HasForeignKey(t => t.SubjectAssignmentId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(t => t.Day)
                .WithMany(d => d.timeTableEntries)
                .HasForeignKey(t => t.DayId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
