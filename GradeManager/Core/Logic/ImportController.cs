using Core.Contracts;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Logic;

namespace Core.Logic
{
    public class ImportController
    {
        const string Filename = "TestDaten.csv";
        private IUnitOfWork UnitOfWork { get; }

        public ImportController(IUnitOfWork unitOfWork) 
        {
            UnitOfWork= unitOfWork;
        }

        #region ReadfromCSV
        public async static Task<IEnumerable<SchoolClass>> ReadSchoolclassesfromCSV()
        {
            var lines = (await File.ReadAllLinesAsync(Filename))
                .Skip(1)
                .Select(l => l.Split(";"))
                .ToList();
           
            var schoolClasses = lines
                .Select(t => new SchoolClass
                {
                    Name = t[4],
                    SchoolLevel = int.Parse(t[5]),
                    SchoolYear = DateTime.Parse(t[6])
                }).ToList();

            return schoolClasses;
        }      
        public async static Task<IEnumerable<Student>> ReadStudentsfromCSV()
        {
            var lines = (await File.ReadAllLinesAsync(Filename))
                .Skip(1)
                .Select(l => l.Split(";"))
                .ToList();

            var students = lines
                .Select(s => new Student
                {
                    Name = s[0]
                }).ToList();

            return students;
        }
        public async static Task<IEnumerable<Teacher>> ReadTeachersfromCSV()
        {
            var lines = (await File.ReadAllLinesAsync(Filename))
                .Skip(1)
                .Select(l => l.Split(";"))
                .ToList();

            var teachers = lines
                .Select(t => new Teacher
                {
                    Name = t[1]
                }).ToList();

            return teachers;
        }
        public async static Task<IEnumerable<Subject>> ReadSubjectsfromCSV()
        {
            var lines = (await File.ReadAllLinesAsync(Filename))
                .Skip(1)
                .Select(l => l.Split(";"))
                .ToList();

            var subjects = lines
            .Select(t => new Subject
            {
                Name = t[2]
            }).ToList();

            return subjects;
        }
        public async static Task<IEnumerable<Grade>> ReadGradesfromCSV()
        {
            var lines = (await File.ReadAllLinesAsync(Filename))
                .Skip(1)
                .Select(l => l.Split(";"))
                .ToList();

            var grades = lines
            .Select(t => new Grade
            {
                Graduate = int.Parse(t[3])
            }).ToList();

            return grades;
        }
        #endregion

        public async Task InitUnitOfWork()
        {
            Console.WriteLine("Import der Testdaten in die Datenbank");
            // var unitOfWork = new UnitOfWork();
            Console.WriteLine("Datenbank löschen");
            await UnitOfWork.DeleteDatabaseAsync();
            Console.WriteLine("Datenbank migrieren");
            await UnitOfWork.MigrateDatabaseAsync();
            Console.WriteLine("Daten werden von Testdaten.csv eingelesen");

            var schoolclasses = await ReadSchoolclassesfromCSV();
            //var students = await ReadStudentsfromCSV();
            //var teachers = await ReadTeachersfromCSV();
            //var subjects = await ReadSubjectsfromCSV();
            //var grades = await ReadGradesfromCSV();

            //if (schoolclasses.Count() == 0)
            //{
            //    Console.WriteLine("!!! Es wurden keine Schoolclasses eingelesen");
            //    return;
            //}

            Console.WriteLine($"  Es wurden {schoolclasses.Count()} Schoolclasses eingelesen!");
            //Console.WriteLine($"  Es wurden {students.Count()} Students eingelesen!");
            //Console.WriteLine($"  Es wurden {teachers.Count()} Teacher eingelesen!");
            //Console.WriteLine($"  Es wurden {subjects.Count()} Subjects eingelesen!");
            //Console.WriteLine($"  Es wurden {grades.Count()} Grades eingelesen!");

            await UnitOfWork.SchoolClassRepository.AddRangeAsync(schoolclasses);
            //await UnitOfWork.StudentRepository.AddRangeAsync(students);
            ////await UnitOfWork.TeacherRepository.AddRangeAsync(teachers);
            //await UnitOfWork.SubjectRepository.AddRangeAsync(subjects);
            //await UnitOfWork.GradeRepository.AddRangeAsync(grades);
            await UnitOfWork.SaveChangesAsync();
        }
    }
}