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
    /// Runs lua-scripts
    /// </summary>
    public class LuaScriptRunner
    {
        private readonly Lua state;

        public LuaScriptRunner()
        {
            this.state = new Lua();
        }

        public Grade RunScript(GradeKey key)
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
