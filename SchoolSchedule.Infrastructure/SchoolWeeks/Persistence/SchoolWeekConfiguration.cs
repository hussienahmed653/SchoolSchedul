using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolSchedule.Domain;

namespace SchoolSchedule.Infrastructure.SchoolWeeks.Persistence
{
    internal class SchoolWeekConfiguration : IEntityTypeConfiguration<SchoolWeek>
    {
        public void Configure(EntityTypeBuilder<SchoolWeek> builder)
        {
            builder.HasKey(sw => sw.SchoolWeekId);

            builder.Property(sw => sw.SchoolWeekId)
                .ValueGeneratedNever();

            builder.Property(sw => sw.SchoolWeekDay)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
