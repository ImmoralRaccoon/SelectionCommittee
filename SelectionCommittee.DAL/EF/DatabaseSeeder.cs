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
                },
                new Assessment
                {
                    Id = 5,
                    Name = "Math",
                    Grade = 10,
                    GradeType = "school",
                    EnrolleeId = 2
                },
                new Assessment
                {
                    Id = 6,
                    Name = "Ukrainian language",
                    Grade = 5,
                    GradeType = "school",
                    EnrolleeId = 2
                },
                new Assessment
                {
                    Id = 7,
                    Name = "English",
                    Grade = 3,
                    GradeType = "school",
                    EnrolleeId = 2
                },
                new Assessment
                {
                    Id = 8,
                    Name = "Ukrainian language",
                    Grade = 9,
                    GradeType = "exam",
                    EnrolleeId = 2
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
                },
                new Enrollee
                {
                    Id = 2,
                    FirstName = "Stolyarenko",
                    LastName = "Vladislav",
                    Patronymic = "Notingem",
                    Email = "vdy23i@gmai.com",
                    Region = "Kupiansk",
                    City = "Kharkiv",
                    SchoolLyceumName = "Shool #2"
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