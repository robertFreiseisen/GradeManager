using CSScriptLib;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using CSScripting;
using System.Diagnostics;

namespace Core.Logic
{
    public class CsScriptRunner
    {
        public static Grade RunScript(GradeKey key, List<Grade> grades)
        {
            var result = new Grade();

            // StringWriter erstellen, um die Ausgabe des Skripts zu erfassen
            StringWriter sw = new StringWriter();

            // Console.Out umleiten
            TextWriter originalOut = Console.Out;
            Console.SetOut(sw);
            try
            {                
                dynamic script = CSScript.Evaluator
                    .ReferenceAssemblyOf(typeof(GradeKey))
                    .ReferenceAssemblyOf(typeof(Grade))
                    .CompileCode(key.Calculation)
                    .CreateObject("*");
                
                var res = script.Calculate(key, grades);
                result.Teacher = key.Teacher;              
                result.Graduate = Convert.ToInt32(res);

            }
            catch (Exception)
            {
                result.Teacher = null;
                result.Graduate = 0;
            }
            finally
            {
                // Output ausgeben
                Debug.WriteLine(sw);
            }

            return result;
        }
    }
}
