using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolSchedule.Domain;

namespace SchoolSchedule.Infrastructure.SubjectAssignments.Persistence
{
    internal class SubjectAssignmentConfiguration : IEntityTypeConfiguration<SubjectAssignment>
    {
        public void Configure(EntityTypeBuilder<SubjectAssignment> builder)
        {
            builder.HasKey(sa => sa.SubjectAssignmentId);

            builder.Property(sa => sa.SubjectAssignmentId)
                   .ValueGeneratedNever();

            builder.Property(sa => sa.SubjectAssignmentGuid)
                   .HasDefaultValueSql("NEWID()");

            builder.Property(sa => sa.EvenOrOdd)
                   .IsRequired();

            builder.Property(sa => sa.Amount)
                   .IsRequired();

            builder.Property(sa => sa.AddedOn)
                .HasDefaultValueSql("GETDATE()");


            builder.HasIndex(sa => new { sa.SubjectId, sa.GradeId, sa.DepartementId, sa.EvenOrOdd, sa.Amount })
                .IsUnique();

            // SubjectAssignment لازم يبقى مربوط بـ Subject, Classe, Departement
            builder.HasOne(sa => sa.Subject)
                   .WithMany(s => s.Assignments)
                   .HasForeignKey(sa => sa.SubjectId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(sa => sa.Grade)
                   .WithMany(c => c.Assignments)
                   .HasForeignKey(sa => sa.GradeId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(sa => sa.Departement)
                   .WithMany(d => d.Assignments)
                   .HasForeignKey(sa => sa.DepartementId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(sa => sa.ClassSection)
           .WithMany(cs => cs.Assignments)
           .HasForeignKey(sa => sa.ClassSectionId);
        }
    }
}
