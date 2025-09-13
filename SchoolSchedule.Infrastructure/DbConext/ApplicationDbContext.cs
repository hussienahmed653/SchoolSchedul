using Microsoft.EntityFrameworkCore;
using SchoolSchedule.Domain;

namespace SchoolSchedule.Infrastructure.DbConext
{
    internal class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Classe> Classes { get; set; }
        public DbSet<Departement> Departements { get; set; }
        public DbSet<SchoolWeek> SchoolWeeks { get; set; }
    }
}
