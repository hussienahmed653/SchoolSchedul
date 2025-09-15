using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolSchedule.Domain;

namespace SchoolSchedule.Infrastructure.Departements.Persistence
{
    internal class DepartmentConfiguration : IEntityTypeConfiguration<Departement>
    {
        public void Configure(EntityTypeBuilder<Departement> builder)
        {
            builder.HasKey(d => d.DepartementId);

            builder.Property(d => d.DepartementId)
                .ValueGeneratedNever();

            builder.Property(d => d.DepartementName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(d => d.DepartementGuid)
                .HasDefaultValueSql("NEWID()");

            builder.HasMany(d => d.Assignments)
               .WithOne(a => a.Departement)
               .HasForeignKey(a => a.DepartementId);

            builder.HasOne(d => d.Classe)
                .WithMany(d => d.Departements)
                .HasForeignKey(d => d.ClasseId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
