using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolSchedule.Domain;

namespace SchoolSchedule.Infrastructure.JopTitles.Persistence
{
    internal class JopTitleConfiguration : IEntityTypeConfiguration<JobTitle>
    {
        public void Configure(EntityTypeBuilder<JobTitle> builder)
        {
            builder.HasKey(j => j.JobTitleId);
            builder.Property(j => j.JobTitleId)
                .ValueGeneratedNever();

            builder.Property(j => j.JobTitleGuid)
                .HasDefaultValueSql("NEWID()");

            builder.Property(j => j.JobTitleName)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
