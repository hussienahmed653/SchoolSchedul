using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolSchedule.Domain;

namespace SchoolSchedule.Infrastructure.ClassSections.Persistence
{
    internal class ClassSectionConfiguration : IEntityTypeConfiguration<ClassSection>
    {
        public void Configure(EntityTypeBuilder<ClassSection> builder)
        {
            builder.HasKey(cs => cs.ClassSectionId);
            builder.Property(cs => cs.ClassSectionId)
                .ValueGeneratedNever();

            builder.Property(cs => cs.ClassSectionGuid)
                .HasDefaultValueSql("NEWID()");

            builder.HasIndex(cs => new { cs.GradeId, cs.SectionName })
                .IsUnique();

            builder.Property(cs => cs.SectionName)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(cs => cs.Grade)
                .WithMany(cs => cs.ClassSections)
                .HasForeignKey(cs => cs.GradeId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
