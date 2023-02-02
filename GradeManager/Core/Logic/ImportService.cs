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
        public List<SchoolClass> ImportSchoolClasses()
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
                SchoolClass schoolClass = new SchoolClass
                {
                    Name = $"{i}BHIF",
                    SchoolLevel= i ,                  
                };
                schoolClasses.Add(schoolClass);
            }

            #region Create Fake Students for each Schoolclass
            for (int i = 0; i < 10; i++)
            {
                var studentFaker = new Faker<Student>()
                .RuleFor(x => x.Name, x => x.Person.FullName).Generate();
                firstStudents.Add(studentFaker);
            }
            schoolClasses.ElementAt(0).Students = firstStudents;

            for (int i = 0; i < 10; i++)
            {
                var studentFaker = new Faker<Student>()
                .RuleFor(x => x.Name, x => x.Person.FullName).Generate();
                secondStudents.Add(studentFaker);
            }
            schoolClasses.ElementAt(1).Students = secondStudents;

            for (int i = 0; i < 10; i++)
            {
                var studentFaker = new Faker<Student>()
                .RuleFor(x => x.Name, x => x.Person.FullName).Generate();
                thirdStudents.Add(studentFaker);
            }
            schoolClasses.ElementAt(2).Students = thirdStudents;

            for (int i = 0; i < 10; i++)
            {
                var studentFaker = new Faker<Student>()
                .RuleFor(x => x.Name, x => x.Person.FullName).Generate();
                fourthStudents.Add(studentFaker);
            }
            schoolClasses.ElementAt(3).Students = fourthStudents;

            for (int i = 0; i < 10; i++)
            {
                var studentFaker = new Faker<Student>()
                .RuleFor(x => x.Name, x => x.Person.FullName).Generate();
                fifthStudents.Add(studentFaker);
            }
            schoolClasses.ElementAt(4).Students = fifthStudents;
            #endregion

            return schoolClasses;
        }
    }
}
