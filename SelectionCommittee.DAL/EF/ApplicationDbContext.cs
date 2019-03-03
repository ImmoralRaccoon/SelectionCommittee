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
            //modelBuilder.Entity<FacultyEnrollee>()
            //    .HasKey(f => new {f.EnrolleeId, f.FacultyId});



            //modelBuilder.Entity<FacultyEnrollee>()
            //    .HasOne(fe => fe.Enrollee)
            //    .WithMany(e => e.FacultyEnrolles)
            //    .HasForeignKey(fe => fe.EnrolleeId);

            //modelBuilder.Entity<FacultyEnrollee>()
            //    .HasOne(fe => fe.Faculty)
            //    .WithMany(f => f.FacultyEnrolles)
            //    .HasForeignKey(fe => fe.FacultyId)
            //    .OnDelete(DeleteBehavior.Restrict);

            DatabaseSeeder.Seed(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
    }
}