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
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Departement> Departements { get; set; }
        public DbSet<SchoolWeek> SchoolWeeks { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<SubjectAssignment> SubjectAssignments { get; set; }
        public DbSet<ClassSection> ClassSection { get; set; }
        public DbSet<JobTitle> JobTitles { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        
    }
}
