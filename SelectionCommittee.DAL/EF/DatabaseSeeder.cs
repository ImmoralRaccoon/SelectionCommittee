using Microsoft.EntityFrameworkCore;
using SelectionCommittee.DAL.Entities;

namespace SelectionCommittee.DAL.EF
{
    public static class DatabaseSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Assessment>().HasData(
                new Assessment
                {
                    Id = 1,
                    Name = "Math",
                    Grade = 10,
                    GradeType = "school",
                    EnrolleeId = 1
                },
                new Assessment
                {
                    Id = 2,
                    Name = "Ukrainian language",
                    Grade = 12,
                    GradeType = "school",
                    EnrolleeId = 1
                },
                new Assessment
                {
                    Id = 3,
                    Name = "English",
                    Grade = 11,
                    GradeType = "school",
                    EnrolleeId = 1
                },
                new Assessment
                {
                    Id = 4,
                    Name = "Ukrainian language",
                    Grade = 12,
                    GradeType = "exam",
                    EnrolleeId = 1
                });

            modelBuilder.Entity<Enrollee>().HasData(
                new Enrollee
                {
                    Id = 1,
                    FirstName = "Clad",
                    LastName = "Dyihcic",
                    Patronymic = "Notingem",
                    Email = "vdyi@gmai.com",
                    Region = "Kharkiv",
                    City = "Kharkiv",
                    SchoolLyceumName = "Shool #12"
                });

            modelBuilder.Entity<Faculty>().HasData(
                new Faculty
                {
                    Id = 1,
                    Name = "Economy",
                    NumberOfPlaces = 123,
                    NumberOfBudgetPlaces = 23
                });
        }
    }
}