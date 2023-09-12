using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using NLua;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace Core.Logic
{
    /// <summary>
    /// Runs Python-scripts
    /// </summary>
    public class PythonScriptRunner
    {
        private readonly ScriptEngine engine;

        public PythonScriptRunner()
        {
            this.engine = Python.CreateEngine();
        }
        public Grade RunScript(GradeKey key, List<Grade> grades)
        {
            if (key.Calculation == string.Empty || key.UsedKinds == null || grades == null)
            {
                throw new NullReferenceException("Not enough information for Calculation");
            }

            var code = key.Calculation;

            var result = new Grade();
            try
            {
                var scope = engine.CreateScope();
                engine.GetClrModule();

                var parseGrades = grades.ToArray()!;
                scope.SetVariable("grades", parseGrades);
                var source = engine.Execute(code, scope);
                var graduate = scope.GetVariable("graduate");
                result.Teacher = key.Teacher;
                result = Convert.ToInt32(graduate);
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }
    }
}
