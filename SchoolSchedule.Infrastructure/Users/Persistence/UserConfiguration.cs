using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolSchedule.Domain;

namespace SchoolSchedule.Infrastructure.Users.Persistence
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.UserId);

            builder.Property(u => u.UserId)
                .ValueGeneratedNever();

            builder.Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(450);

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.Property(u => u.PasswordHash)
                .IsRequired();
        }
    }
}
