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

        public async static Task<IEnumerable<SchoolClass>> ReadFromCSV()
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
            var teachers = lines
                .Select(t => new Teacher
                {
                    Name = t[1]
                }).ToList();
            var subjects = lines
                .Select(t => new Subject
                {
                    Name = t[2]
                }).ToList();
            var grades = lines
                .Select(t => new Grade
                {
                    Graduate = int.Parse(t[3])
                }).ToList();
            var schoolClasses = lines
                .Select(t => new SchoolClass
                {
                    Name = t[4]
                }).ToList();

            return schoolClasses;
        }

        //public async Task ReadFromCSV()
        //{
        //    var lines = (await File.ReadAllLinesAsync(Filename))
        //        .Skip(1)
        //        .Select(l => l.Split(";"))
        //        .ToList();

        //    var students = lines
        //        .Select(s => new Student
        //        {
        //            Name = s[0]
        //        }).ToList();
        //    var teachers = lines
        //        .Select(t => new Teacher
        //        {
        //            Name = t[1]
        //        }).ToList();
        //    var subjects = lines
        //        .Select(t => new Subject
        //        {
        //            Name = t[2]
        //        }).ToList();
        //    var grades = lines
        //        .Select(t => new Grade
        //        {
        //            Graduate = int.Parse(t[3])
        //        }).ToList();
        //    var schoolClasses = lines
        //        .Select(t => new SchoolClass
        //        {
        //            Name = t[4]
        //        }).ToList();

        //    Console.WriteLine("Import der Testdaten in die Datenbank");
        //    // var unitOfWork = new UnitOfWork();
        //    Console.WriteLine("Datenbank löschen");
        //    await UnitOfWork.DeleteDatabaseAsync();
        //    Console.WriteLine("Datenbank migrieren");
        //    await UnitOfWork.MigrateDatabaseAsync();
        //    Console.WriteLine("Daten werden von Testdaten.csv eingelesen");           

        //    await UnitOfWork.SchoolClassRepository.AddRangeAsync(schoolClasses);
        //    await UnitOfWork.StudentRepository.AddRangeAsync(students);
        //    //await UnitOfWork.TeacherRepository.AddRangeAsync(schoolClasses);
        //    await UnitOfWork.SubjectRepository.AddRangeAsync(subjects);
        //    await UnitOfWork.GradeRepository.AddRangeAsync(grades);

        //    await UnitOfWork.SaveChangesAsync();
        //}

        public async Task InitUnitOfWork()
        {
            Console.WriteLine("Import der Testdaten in die Datenbank");
            // var unitOfWork = new UnitOfWork();
            Console.WriteLine("Datenbank löschen");
            await UnitOfWork.DeleteDatabaseAsync();
            Console.WriteLine("Datenbank migrieren");
            await UnitOfWork.MigrateDatabaseAsync();
            Console.WriteLine("Daten werden von Testdaten.csv eingelesen");
            var schoolclasses = await ReadFromCSV();
            if (schoolclasses.Count() == 0)
            {
                Console.WriteLine("!!! Es wurden keine Schoolclasses eingelesen");
                return;
            }

            Console.WriteLine($"  Es wurden {schoolclasses.Count()} Schoolclasses eingelesen!");
            await UnitOfWork.SchoolClassRepository.AddRangeAsync(schoolclasses);
            await UnitOfWork.SaveChangesAsync();
        }
    }
}