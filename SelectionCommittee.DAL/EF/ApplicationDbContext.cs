using Microsoft.EntityFrameworkCore;
using SelectionCommittee.DAL.Entities;

namespace SelectionCommittee.DAL.EF
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Assessment> Assessments { get; set; }
        public DbSet<Enrollee> Enrollees { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<FacultyEnrollee> FacultyEnrollees { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            DatabaseSeeder.Seed(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
    }
}