﻿using Bogus;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Shared.Entities;

namespace Core.Logic
{
    /// <summary>
    /// Generating Realistic Fake Data in .NET using Bogus
    /// </summary>
    public class ImportService
    {
        public ImportService(ApplicationDbContext dbContext) 
        {
            this.DbContext = dbContext;
               
        }
        private ApplicationDbContext DbContext { get; }

        /// <summary>
        /// Generates Fake Schoolclasses including Students
        /// </summary>
        /// <returns></returns>
        private List<SchoolClass> GenerateSchoolClasses()
        {
            List<Student> firstStudents = new List<Student>();
            List<Student> secondStudents = new List<Student>();
            List<Student> thirdStudents = new List<Student>();
            List<Student> fourthStudents = new List<Student>();
            List<Student> fifthStudents = new List<Student>();

            // Create 5 Test-Schoolclasses
            List<SchoolClass> schoolClasses = new List<SchoolClass>();
            for (int i = 1; i < 6; i++)
            {
                SchoolClass schoolClass = new SchoolClass { Name = $"{i}BHIF", SchoolLevel = i, SchoolYear="2022/23"};
                schoolClasses.Add(schoolClass);
            }

            #region Create Fake Students for each Schoolclass
            for (int i = 0; i < 10; i++)
            {
                var studentFaker = new Faker<Student>()
                    .RuleFor(x => x.Name, x => x.Person.FullName)
                    .Generate();
                firstStudents.Add(studentFaker);
            }
            schoolClasses.ElementAt(0).Students = firstStudents;

            for (int i = 0; i < 10; i++)
            {
                var studentFaker = new Faker<Student>()
                    .RuleFor(x => x.Name, x => x.Person.FullName)
                    .Generate();
                secondStudents.Add(studentFaker);
            }
            schoolClasses.ElementAt(1).Students = secondStudents;

            for (int i = 0; i < 10; i++)
            {
                var studentFaker = new Faker<Student>()
                    .RuleFor(x => x.Name, x => x.Person.FullName)
                    .Generate();
                thirdStudents.Add(studentFaker);
            }
            schoolClasses.ElementAt(2).Students = thirdStudents;

            for (int i = 0; i < 10; i++)
            {
                var studentFaker = new Faker<Student>()
                    .RuleFor(x => x.Name, x => x.Person.FullName)
                    .Generate();
                fourthStudents.Add(studentFaker);
            }
            schoolClasses.ElementAt(3).Students = fourthStudents;

            for (int i = 0; i < 10; i++)
            {
                var studentFaker = new Faker<Student>()
                    .RuleFor(x => x.Name, x => x.Person.FullName)
                    .Generate();
                fifthStudents.Add(studentFaker);
            }
            schoolClasses.ElementAt(4).Students = fifthStudents;
            #endregion
            return schoolClasses;
        }

        /// <summary>
        /// Import Schoolclasses in Db
        /// </summary>
        /// <returns></returns>
        public async Task ImportSchoolClassesAsync()
        {
            await DbContext.SchoolClasses.AddRangeAsync(GenerateSchoolClasses());
            await DbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Import Teachers in Db
        /// </summary>
        /// <returns></returns>
        public async Task ImportTeachersAsync()
        {
            var teachers = await GenerateTeacherAsync();
            await DbContext.Teachers.AddRangeAsync(teachers);
            await DbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Generates Fake Teachers
        /// </summary>
        /// <returns></returns>
        private async Task<List<Teacher>> GenerateTeacherAsync()
        {
            List<Teacher> teachers = new List<Teacher>();

            for (int i = 0; i < 5; i++)
            {
                var fakeTeacher = new Faker<Teacher>()
                    .RuleFor(x => x.Name, x => x.Person.FullName)
                    .Generate();
                fakeTeacher.Subjects = await GetRandomSubjectsAsync(2);
                fakeTeacher.SchoolClasses = await GetRandomSchoolclassAsync(4);

                teachers.Add(fakeTeacher);
                await DbContext.SaveChangesAsync();
            }

            return teachers;
        }

        private async Task<List<SchoolClass>> GetRandomSchoolclassAsync(int quantity)
        {
            var scs = new List<SchoolClass>();
            var allSchoolclasses = await DbContext.SchoolClasses.ToArrayAsync();
            for (int i = 0; i < quantity; i++)
            {
                var ran = allSchoolclasses[Random.Shared.Next(0, allSchoolclasses.Length - 1)];
                var contains = scs.Find(s => s.Name == ran.Name);
                if (contains == null)
                {
                    scs.Add(ran);
                }
                else
                {
                    i--;
                }
            }

            return scs;
        }

        /// <summary>
        /// Get Random Subjects
        /// </summary>
        /// <param name="quantity"></param>
        /// <returns></returns>
        private async Task<List<Subject>> GetRandomSubjectsAsync(int quantity)
        {
            List<Subject> subjects = new List<Subject>();

            var allSubjects = await DbContext.Subjects.ToArrayAsync();
            for (int i = 0; i < quantity; i++)
            {
                var randomSubject = allSubjects[Random.Shared.Next(0, allSubjects.Length)];
                var contains = subjects.Find(s => s.Name == randomSubject.Name);
                if (!subjects.Contains(randomSubject))
                {
                    subjects.Add(randomSubject);                                    
                }
                else
                {
                    i--;
                }
            }

            return subjects;
        }

        /// <summary>
        /// Import Subjects in Db
        /// </summary>
        /// <returns></returns>
        public async Task ImportSubjectsAsync()
        {
            var allSubjects = new List<Subject>
            {
                new Subject { Name = "Mathematik" },
                new Subject { Name = "Deutsch" },
                new Subject { Name = "English" },
                new Subject { Name = "Programmieren" },
                new Subject { Name = "Datenbanken" },
                new Subject { Name = "Netzwerktechnik" },
                new Subject { Name = "Religion" },
                new Subject { Name = "Recht" },
                new Subject { Name = "Sport" },
                new Subject { Name = "Geschichte" }
            };

            await DbContext.Subjects.AddRangeAsync(allSubjects);

            await DbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Import Grades to Students in db
        /// </summary>
        /// <returns></returns>
        public async Task ImportGradesToStudentsAsync()
        {
            var schoolClasses = await DbContext.SchoolClasses
                .Include(s => s.Students)
                .ToListAsync();
            var subjects = await DbContext.Subjects.ToListAsync();
            var teachers = await DbContext.Teachers.Include(t => t.Subjects).ToListAsync();

            foreach (var teacher in teachers)
            {
                foreach (var subject in teacher.Subjects)
                {
                    foreach (var schoolClass in schoolClasses)
                    {
                        var ran = new Random();

                        var studentsFromClass = schoolClass.Students.ToList();
                        foreach (var student in studentsFromClass)
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                Grade grade = new Grade
                                {
                                    GradeKind = await DbContext.GradeKinds.SingleAsync(
                                        k => k.Name == "MAK"
                                    ),
                                    Student = student,
                                    Note = "Testdaten",
                                    Subject = subject,
                                    Teacher = teacher,
                                    Graduate = ran.Next(1, 5)
                                };
                                await DbContext.Grades.AddAsync(grade);
                            }
                            for (int i = 0; i < 5; i++)
                            {
                                Grade grade = new Grade
                                {
                                    GradeKind = await DbContext.GradeKinds.SingleAsync(
                                        k => k.Name == "TEST"
                                    ),
                                    Student = student,
                                    Note = "Testdaten",
                                    Subject = subject,
                                    Teacher = teacher,
                                    Graduate = ran.Next(1, 5)
                                };
                                await DbContext.Grades.AddAsync(grade);
                            }

                            for (int i = 0; i < 5; i++)
                            {
                                Grade grade = new Grade
                                {
                                    GradeKind = await DbContext.GradeKinds.SingleAsync(
                                        k => k.Name == "HOMEWORK"
                                    ),
                                    Student = student,
                                    Note = "Testdaten",
                                    Subject = subject,
                                    Teacher = teacher,
                                    Graduate = ran.Next(1, 5)
                                };
                                await DbContext.Grades.AddAsync(grade);
                            }
                        }
                    }
                }
            }

            await DbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Generate Different GradeKinds and import in Db
        /// </summary>
        /// <returns></returns>
        public async Task ImportGradeKindsAsync()
        {
            var gradeKinds = new GradeKind[]
            {
                new() { Name = "YEAR" },
                new() { Name = "MAK" },
                new() { Name = "TEST" },
                new() { Name = "HOMEWORK" }
            };

            await DbContext.GradeKinds.AddRangeAsync(gradeKinds);
            await DbContext.SaveChangesAsync();
        }
    }
}
