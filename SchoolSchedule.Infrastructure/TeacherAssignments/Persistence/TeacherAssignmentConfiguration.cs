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

            builder.Property(t => t.SubjectAssignmentId)
                .IsRequired(false);

            builder.HasIndex(t => new { t.TeacherId, t.SubjectAssignmentId, t.ClassSectionId })
                .IsUnique();

            builder.HasOne(t => t.Teacher)
                .WithMany(t => t.TeacherAssignments)
                .HasForeignKey(t => t.TeacherId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(t => t.SubjectAssignment)
                .WithMany(t => t.TeacherAssignments)
                .HasForeignKey(t => t.SubjectAssignmentId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(t => t.ClassSection)
                .WithMany(t => t.TeacherAssignments)
                .HasForeignKey(t => t.ClassSectionId)
                .OnDelete(DeleteBehavior.NoAction);
            
        }
    }
}
