using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolSchedule.Domain;

namespace SchoolSchedule.Infrastructure.Departements
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

            builder.HasOne(d => d.Classe)
                .WithMany(d => d.Departements)
                .HasForeignKey(d => d.ClasseId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
