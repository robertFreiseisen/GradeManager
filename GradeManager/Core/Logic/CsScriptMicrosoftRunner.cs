using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Shared.Entities;

namespace Core.Logic
{
    public class CsScriptMicrosoftRunner
    {
        public async Task<Grade> RunScriptAsync(GradeKey key, List<Grade> grades)
        {
            var result = new Grade();
            var globals = new Globals { GradeKey = key, Grades = grades };
            var options = ScriptOptions.Default
                .WithEmitDebugInformation(true);

            try
            {
                var state = await CSharpScript.RunAsync(key.Calculation, options, globals: globals);
                WriteAllVariablesFromScript(state.Variables);
                                              
                result.Teacher = key.Teacher;
                result.Graduate = Convert.ToInt32(state.ReturnValue);

            }
            catch (Exception)
            {
                result.Teacher = null;
                result.Graduate = 0;
            }

            return result;
        }

        private static void WriteAllVariablesFromScript(ImmutableArray<ScriptVariable> variables)
        {
            foreach (var variable in variables)
            {
                Debug.WriteLine(variable.Name + ": " + variable.Value);
            }
        }

        public class Globals
        {
            public GradeKey? GradeKey;
            public List<Grade>? Grades;
        }
    }
}
