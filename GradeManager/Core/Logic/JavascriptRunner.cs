using JavaScriptEngineSwitcher.Jint;
using Jint;
using Jint.Native;
using Jint.Native.Object;
using Jint.Runtime;
using Jint.Runtime.Interop;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Newtonsoft.Json;
using Shared.Entities;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.ConstrainedExecution;

namespace Core.Logic
{
    public class JavascriptRunner
    {
        public static Grade RunScript(GradeKey key, List<Grade> grades)
        {
            var engine = new JintJsEngine();               
            Grade result = new Grade();
            List<string> logs = new List<string>();

            try
            {
                var gradeKindsList = JsonConvert.SerializeObject(key.UsedKinds);
                var gradesList = JsonConvert.SerializeObject(grades);                

                engine.SetVariableValue("gradeKindsList", gradeKindsList);
                engine.SetVariableValue("gradesList", gradesList);               

                if (key.Calculation != null)
                {
                    engine.Execute(key.Calculation);                   
                }                          

                // Get Return from Script
                var resultGrade = engine.GetVariableValue("result");
                               
                result.Teacher = key.Teacher;
                result.Graduate = Convert.ToInt32(resultGrade);
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
