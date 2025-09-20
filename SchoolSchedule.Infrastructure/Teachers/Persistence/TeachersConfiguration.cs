using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolSchedule.Domain;

namespace SchoolSchedule.Infrastructure.Teachers.Persistence
{
    internal class TeachersConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.HasKey(t => t.TeacherId);

            builder.Property(t => t.TeacherId)
                .ValueGeneratedNever();

            builder.Property(t => t.TeacherGuid)
                .HasDefaultValueSql("NEWID()");

            builder.Property(t => t.TeacherName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(t => t.BirthDate)
                .HasDefaultValueSql("GETDATE()");

            builder.Property(t => t.HireDate)
                .HasDefaultValueSql("GETDATE()");

            builder.Property(t => t.MinistryStartDate)
                .HasDefaultValueSql("GETDATE()");
            
            builder.Property(t => t.SchoolStartDate)
                .HasDefaultValueSql("GETDATE()");

            builder.Property(t => t.AddedOn)
                .HasDefaultValueSql("GETDATE()");

            builder.Property(t => t.IsActive)
                .HasDefaultValue(1);

            builder.Property(t => t.WorkType)
                .HasConversion<string>()
                .HasMaxLength(50);

            builder.ToTable(t =>
            {
                t.HasCheckConstraint("CHECK_IF_WORKTYPE_IS_FULLTIME_OR_PARTTIME", "[WorkType] IN (N'كلي',N'جزئي')");
            });

            builder.HasOne(t => t.JobTitle)
                .WithMany(t => t.Teachers)
                .HasForeignKey(t => t.JobTitleId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
