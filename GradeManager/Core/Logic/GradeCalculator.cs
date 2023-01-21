using Core.Contracts;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Core.Logic
{
    public class GradeCalculator
    {
        // Logic to Run the scripts

        public async Task<List<Grade>?> CalculateKeysForClassAndSubject(int schoolClassId, int subject, IUnitOfWork uow)
        {
            var grades = await (uow.GradeRepository.GetByClassAndSubjectAsync(schoolClassId, subject));

            if (grades == null || grades.Count() == 0)
            {
                return null;
            }

            var g1 = grades.First();
            var key = await uow.GradeKeyRepository.GetByTeacherAndSubjectAsync(g1.TeacherId, g1.SubjectId);

            if (key == null)
            {
                return null;
            }

            var result = new List<Grade>();

            foreach (var item in grades.GroupBy(g => g.Student))
            {
                var gradeToAdd = RunScript(key, item.ToList());
                gradeToAdd.Student = item.Key;
                gradeToAdd.Note = "Total Graduate";
                gradeToAdd.Teacher = key.Teacher;
                result.Add(gradeToAdd);
            }

            return result; 
        }

        private Grade RunScript(GradeKey gradeKey, List<Grade> studentGrades)
        {
            var result = new Grade();
            switch (gradeKey.ScriptType)
            {
                case ScriptType.None:
                    result = CalcDefaultKey(studentGrades);
                    break;
                case ScriptType.Lua:
                    result = LuaScriptRunner.RunScript(gradeKey, studentGrades);
                    break;
                case ScriptType.Python:
                    break;
                case ScriptType.JavaScript:
                    break;
                case ScriptType.CSharpScript:
                    break;
                default:
                    break;
            }

            return result;
        }

        private Grade CalcDefaultKey(List<Grade> grades)
        {
            var result = grades.Sum(g => g.Graduate) / grades.Count();

            return new Grade { };

        }

    }
}
