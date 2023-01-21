using NLua;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Logic
{
    internal static class LuaScriptRunner
    {
        private static Lua state = new Lua();

        public static Grade RunScript(GradeKey key, List<Grade> gradesPerStudent)
        {
            if (key.Calculation == string.Empty || key.UsedKinds == null)
            {
                return null;
            }

            var code = key.Calculation;

            if (CompareKindsAndScript(code, key.UsedKinds.ToList()))
            {
                return null;
            }

            var result = new Grade();

            foreach (var item in gradesPerStudent)
            {

                try
                {
                    state.DoString(code);
                    state.DoString(@"graduate = calculate()");
                    result.Teacher = key.Teacher;
                    var gr = state["graduate"];
                    if (gr != null)
                    {
                        result.Graduate = Convert.ToInt32(gr);
                    }
                }
                catch (Exception)
                {

                    throw;
                }

            }
            

            return result;
        }

        private static bool CompareKindsAndScript(string? code, IList<GradeKind> gradeKinds)
        {
            foreach (var item in gradeKinds)
            {
                if (code!.Contains(item.Name))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
