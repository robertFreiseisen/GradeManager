using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSScripting;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Shared.Entities;

namespace Core.Logic
{
    public class CsScriptMicrosoftRunner
    {
        public static async Task<Grade> RunScriptAsync(GradeKey key, List<Grade> grades)
        {
            var result = new Grade();

            var globals = new Globals { GradeKey = key, Grades = grades };
            var state = await CSharpScript.RunAsync(key.Calculation, globals: globals);

            double resultGrade = 0.0;
            foreach (var variable in state.Variables)
            {
                if (variable.Name == "result")
                {
                    resultGrade = (double)variable.Value;
                }
            }

            result.Teacher = key.Teacher;
            result.Graduate = Convert.ToInt32(resultGrade);

            return result;
        }
        public class Globals
        {
            public GradeKey? GradeKey;
            public List<Grade>? Grades;
        }
    }
}
