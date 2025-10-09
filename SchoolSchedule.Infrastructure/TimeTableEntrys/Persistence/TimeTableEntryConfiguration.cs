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

            builder.ToTable(t =>
            {
                t.HasCheckConstraint("CK_TimeTableEntry_PeriodRange", "Period >= 1 AND Period <= 8");
            });

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
