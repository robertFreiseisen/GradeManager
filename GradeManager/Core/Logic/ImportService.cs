using Bogus;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Logic
{
    /// <summary>
    /// Generating Realistic Fake Data in .NET using Bogus
    /// </summary>
    public class ImportService
    {
        public List<SchoolClass> ImportBogusData()
        {
            /*var gradeFaker = new Faker<Grade>()
                .RuleFor(x => x.Graduate, x => x.PickRandom(1, 6));*/
            List<Student> students = new List<Student>();
            List<SchoolClass> schoolClasses = new List<SchoolClass>();

            for (int i = 0; i < 10; i++)
            {
                var studentFaker = new Faker<Student>()
                .RuleFor(x => x.Name, x => x.Person.FullName).Generate();
                students.Add(studentFaker);
            }

            for (int i = 1; i < 6; i++)
            {
                SchoolClass schoolClass = new SchoolClass
                {
                    Name = $"{i}BHIF",
                    SchoolLevel= i ,
                    Students = students,
                };
                schoolClasses.Add(schoolClass);
            }

            return schoolClasses;
        }
    }
}
