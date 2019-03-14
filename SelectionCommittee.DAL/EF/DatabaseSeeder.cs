using Microsoft.EntityFrameworkCore;
using SelectionCommittee.DAL.Entities;

namespace SelectionCommittee.DAL.EF
{
    public static class DatabaseSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            // Faculties
            modelBuilder.Entity<Faculty>().HasData(
                new Faculty
                {
                    Id = 1,
                    Name = "Faculty of Economics",
                    NumberOfPlaces = 4,
                    NumberOfBudgetPlaces = 2
                },
                new Faculty
                {
                    Id = 2,
                    Name = "Faculty of Physics and Mathematics",
                    NumberOfPlaces = 3,
                    NumberOfBudgetPlaces = 1
                },
                new Faculty
                {
                    Id = 3,
                    Name = "Faculty of History",
                    NumberOfPlaces = 5,
                    NumberOfBudgetPlaces = 3
                },
                new Faculty
                {
                    Id = 4,
                    Name = "Faculty of Computer Science",
                    NumberOfPlaces = 6,
                    NumberOfBudgetPlaces = 5
                },
                new Faculty
                {
                    Id = 5,
                    Name = "Faculty of Geology",
                    NumberOfPlaces = 3,
                    NumberOfBudgetPlaces = 1
                });

            // Enrollees
            modelBuilder.Entity<Enrollee>().HasData(
                new Enrollee
                {
                    Id = 1,
                    FirstName = "Kirianenko",
                    LastName = "Vladislav",
                    Patronymic = "Igorevich",
                    Email = "kirianenko.vladislav@gmail.com",
                    Region = "Kiev",
                    City = "Kiev",
                    SchoolLyceumName = "Ukrainian Humanities Lyceum"
                },
                new Enrollee
                {
                    Id = 2,
                    FirstName = "Pidkopay",
                    LastName = "Ivan",
                    Patronymic = "Vitalievich",
                    Email = "pidkop@gmail.com",
                    Region = "Kharkiv",
                    City = "Kupiansk",
                    SchoolLyceumName = "School №1"
                },
                new Enrollee
                {
                    Id = 3,
                    FirstName = "Nesterenko",
                    LastName = "Oleksiy",
                    Patronymic = "Genadievich",
                    Email = "alex@gmail.com",
                    Region = "Kiev",
                    City = "Kiev",
                    SchoolLyceumName = "School №123"
                },
                new Enrollee
                {
                    Id = 4,
                    FirstName = "Sologubchenko",
                    LastName = "Ivanka",
                    Patronymic = "Egorovna",
                    Email = "sologor@gmail.com",
                    Region = "Dnipro",
                    City = "Kamenskoe",
                    SchoolLyceumName = "Lyceum №33"
                },
                new Enrollee
                {
                    Id = 5,
                    FirstName = "Stadchenko",
                    LastName = "Marianna",
                    Patronymic = "Igorevna",
                    Email = "marishka@gmail.com",
                    Region = "Kharkiv",
                    City = "Kharkiv",
                    SchoolLyceumName = "School №12"
                }
                );

            // Assessments
            modelBuilder.Entity<Assessment>().HasData(
                new Assessment
                {
                    Id = 1,
                    Name = "Ukrainian language",
                    Grade = 10,
                    GradeType = "school",
                    EnrolleeId = 1
                },
                new Assessment
                {
                    Id = 2,
                    Name = "Math",
                    Grade = 12,
                    GradeType = "school",
                    EnrolleeId = 1
                },
                new Assessment
                {
                    Id = 3,
                    Name = "English language",
                    Grade = 11,
                    GradeType = "school",
                    EnrolleeId = 1
                },
                new Assessment
                {
                    Id = 4,
                    Name = "Geography",
                    Grade = 12,
                    GradeType = "shool",
                    EnrolleeId = 1
                },
                new Assessment
                {
                    Id = 5,
                    Name = "History of Ukraine",
                    Grade = 10,
                    GradeType = "school",
                    EnrolleeId = 1
                },
                new Assessment
                {
                    Id = 6,
                    Name = "Ukrainian language",
                    Grade = 11,
                    GradeType = "exam",
                    EnrolleeId = 1
                },
                new Assessment
                {
                    Id = 7,
                    Name = "Math",
                    Grade = 11,
                    GradeType = "exam",
                    EnrolleeId = 1
                },
                new Assessment
                {
                    Id = 8,
                    Name = "Geography",
                    Grade = 12,
                    GradeType = "exam",
                    EnrolleeId = 1
                },

                new Assessment
                {
                    Id = 9,
                    Name = "Ukrainian language",
                    Grade = 10,
                    GradeType = "school",
                    EnrolleeId = 2
                },
                new Assessment
                {
                    Id = 10,
                    Name = "Math",
                    Grade = 9,
                    GradeType = "school",
                    EnrolleeId = 2
                },
                new Assessment
                {
                    Id = 11,
                    Name = "English language",
                    Grade = 11,
                    GradeType = "school",
                    EnrolleeId = 2
                },
                new Assessment
                {
                    Id = 12,
                    Name = "Geography",
                    Grade = 7,
                    GradeType = "shool",
                    EnrolleeId = 2
                },
                new Assessment
                {
                    Id = 13,
                    Name = "History of Ukraine",
                    Grade = 8,
                    GradeType = "school",
                    EnrolleeId = 2
                },
                new Assessment
                {
                    Id = 14,
                    Name = "Ukrainian language",
                    Grade = 10,
                    GradeType = "exam",
                    EnrolleeId = 2
                },
                new Assessment
                {
                    Id = 15,
                    Name = "Math",
                    Grade = 10,
                    GradeType = "exam",
                    EnrolleeId = 2
                },
                new Assessment
                {
                    Id = 16,
                    Name = "Physics",
                    Grade = 7,
                    GradeType = "exam",
                    EnrolleeId = 2
                },

                new Assessment
                {
                    Id = 17,
                    Name = "Ukrainian language",
                    Grade = 5,
                    GradeType = "school",
                    EnrolleeId = 3
                },
                new Assessment
                {
                    Id = 18,
                    Name = "Math",
                    Grade = 9,
                    GradeType = "school",
                    EnrolleeId = 3
                },
                new Assessment
                {
                    Id = 19,
                    Name = "English language",
                    Grade = 11,
                    GradeType = "school",
                    EnrolleeId = 3
                },
                new Assessment
                {
                    Id = 20,
                    Name = "Geography",
                    Grade = 7,
                    GradeType = "shool",
                    EnrolleeId = 3
                },
                new Assessment
                {
                    Id = 21,
                    Name = "History of Ukraine",
                    Grade = 12,
                    GradeType = "school",
                    EnrolleeId = 3
                },
                new Assessment
                {
                    Id = 22,
                    Name = "Ukrainian language",
                    Grade = 10,
                    GradeType = "exam",
                    EnrolleeId = 3
                },
                new Assessment
                {
                    Id = 23,
                    Name = "History",
                    Grade = 5,
                    GradeType = "exam",
                    EnrolleeId = 3
                },
                new Assessment
                {
                    Id = 24,
                    Name = "English language",
                    Grade = 7,
                    GradeType = "exam",
                    EnrolleeId = 3
                },

                new Assessment
                {
                    Id = 25,
                    Name = "Ukrainian language",
                    Grade = 10,
                    GradeType = "school",
                    EnrolleeId = 4
                },
                new Assessment
                {
                    Id = 26,
                    Name = "Math",
                    Grade = 12,
                    GradeType = "school",
                    EnrolleeId = 4
                },
                new Assessment
                {
                    Id = 27,
                    Name = "English language",
                    Grade = 11,
                    GradeType = "school",
                    EnrolleeId = 4
                },
                new Assessment
                {
                    Id = 28,
                    Name = "Geography",
                    Grade = 7,
                    GradeType = "shool",
                    EnrolleeId = 4
                },
                new Assessment
                {
                    Id = 29,
                    Name = "History of Ukraine",
                    Grade = 8,
                    GradeType = "school",
                    EnrolleeId = 4
                },
                new Assessment
                {
                    Id = 30,
                    Name = "Ukrainian language",
                    Grade = 10,
                    GradeType = "exam",
                    EnrolleeId = 4
                },
                new Assessment
                {
                    Id = 31,
                    Name = "Math",
                    Grade = 6,
                    GradeType = "exam",
                    EnrolleeId = 4
                },
                new Assessment
                {
                    Id = 32,
                    Name = "English language",
                    Grade = 7,
                    GradeType = "exam",
                    EnrolleeId = 4
                },

                new Assessment
                {
                    Id = 33,
                    Name = "Ukrainian language",
                    Grade = 11,
                    GradeType = "school",
                    EnrolleeId = 5
                },
                new Assessment
                {
                    Id = 34,
                    Name = "Math",
                    Grade = 11,
                    GradeType = "school",
                    EnrolleeId = 5
                },
                new Assessment
                {
                    Id = 35,
                    Name = "English language",
                    Grade = 12,
                    GradeType = "school",
                    EnrolleeId = 5
                },
                new Assessment
                {
                    Id = 36,
                    Name = "Geography",
                    Grade = 12,
                    GradeType = "shool",
                    EnrolleeId = 5
                },
                new Assessment
                {
                    Id = 37,
                    Name = "History of Ukraine",
                    Grade = 10,
                    GradeType = "school",
                    EnrolleeId = 5
                },
                new Assessment
                {
                    Id = 38,
                    Name = "Ukrainian language",
                    Grade = 11,
                    GradeType = "exam",
                    EnrolleeId = 5
                },
                new Assessment
                {
                    Id = 39,
                    Name = "English language",
                    Grade = 11,
                    GradeType = "exam",
                    EnrolleeId = 5
                },
                new Assessment
                {
                    Id = 40,
                    Name = "Geography",
                    Grade = 12,
                    GradeType = "exam",
                    EnrolleeId = 5
                });
        }
    }
}