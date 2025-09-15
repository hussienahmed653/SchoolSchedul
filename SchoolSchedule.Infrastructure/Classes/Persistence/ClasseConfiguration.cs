using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolSchedule.Domain;

namespace SchoolSchedule.Infrastructure.Classes.Persistence
{
    internal class ClasseConfiguration : IEntityTypeConfiguration<Classe>
    {
        public void Configure(EntityTypeBuilder<Classe> builder)
        {
            builder.HasKey(c => c.ClasseId);

            builder.Property(c => c.ClasseId)
                .ValueGeneratedNever();
            builder.Property(c => c.ClasseYear)
                .HasMaxLength(50);

            builder.Property(c => c.ClasseGuid)
                .HasDefaultValueSql("NEWID()");

            builder.HasMany(c => c.Assignments)
               .WithOne(a => a.Classe)
               .HasForeignKey(a => a.ClasseId);
        }
    }
}
