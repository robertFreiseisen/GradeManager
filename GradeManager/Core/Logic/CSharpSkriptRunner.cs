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
    public class CSharpSkriptRunner
    {
        public static Grade RunScript(GradeKey key, List<Grade> grades)
        {
            var result = new Grade();
            var res = CSScript.Evaluator.LoadCode(key.Calculation);

            return result;
        }
    }
}
