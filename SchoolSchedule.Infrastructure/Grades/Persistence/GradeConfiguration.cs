using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolSchedule.Domain;

namespace SchoolSchedule.Infrastructure.Classes.Persistence
{
    internal class GradeConfiguration : IEntityTypeConfiguration<Grade>
    {
        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            builder.HasKey(c => c.GradeId);

            builder.Property(c => c.GradeId)
                .ValueGeneratedNever();
            builder.Property(c => c.GradeYear)
                .HasMaxLength(50);

            builder.Property(c => c.GradeGuid)
                .HasDefaultValueSql("NEWID()");

            builder.HasMany(c => c.Assignments)
               .WithOne(a => a.Grade)
               .HasForeignKey(a => a.GradeId)
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
