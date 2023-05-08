using CSScriptLib;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Logic
{
    public class CsScriptRunner
    {
        public static Grade RunScript(GradeKey key, List<Grade> grades)
        {
            var result = new Grade();
            try
            {
                dynamic script = CSScript.Evaluator.LoadCode(key.Calculation);
                int res = script.Calculate(1);
            }
            catch (Exception)
            {
                result.Teacher = null;
                result.Graduate = 0;
            }

            return result;
        }
    }
}
