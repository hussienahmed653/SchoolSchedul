using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolSchedule.Domain;

namespace SchoolSchedule.Infrastructure.Subjects.Persistence
{
    internal class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.HasKey(s => s.SubjectId);

            builder.Property(s => s.SubjectId)
                .ValueGeneratedNever();

            builder.Property(s => s.SubjectName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(s => s.SubjectGuid)
                .HasDefaultValueSql("NEWID()");

            builder.Property(s => s.FixedDay)
                .HasDefaultValue(false);

            builder.HasMany(s => s.Assignments)
               .WithOne(a => a.Subject)
               .HasForeignKey(a => a.SubjectId);
        }
    }
}
