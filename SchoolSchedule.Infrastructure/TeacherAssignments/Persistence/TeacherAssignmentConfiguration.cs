using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolSchedule.Domain;

namespace SchoolSchedule.Infrastructure.TeacherAssignments.Persistence
{
    internal class TeacherAssignmentConfiguration : IEntityTypeConfiguration<TeacherAssignment>
    {
        public void Configure(EntityTypeBuilder<TeacherAssignment> builder)
        {
            builder.HasKey(t => t.TeacherAssignmentId);

            builder.Property(t => t.TeacherAssignmentId)
                .ValueGeneratedNever();

            builder.Property(t => t.TeacherAssignmentGuid)
                .IsRequired()
                .HasDefaultValueSql("NEWID()");

            builder.HasIndex(t => new { t.TeacherId, t.SubjectId, t.GradeId, t.ClassSectionId })
                .IsUnique();

            builder.HasOne(t => t.Teacher)
                .WithMany(t => t.TeacherAssignments)
                .HasForeignKey(t => t.TeacherId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(t => t.Subject)
                .WithMany(t => t.TeacherAssignments)
                .HasForeignKey(t => t.SubjectId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(t => t.ClassSection)
                .WithMany(t => t.TeacherAssignments)
                .HasForeignKey(t => t.ClassSectionId)
                .OnDelete(DeleteBehavior.NoAction);
            
            builder.HasOne(t => t.Grade)
                .WithMany(t => t.TeacherAssignments)
                .HasForeignKey(t => t.GradeId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
