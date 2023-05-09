using Jint;
using Jint.Native;
using Jint.Native.Object;
using Jint.Runtime;
using Jint.Runtime.Debugger;
using Jint.Runtime.Interop;
using Shared.Entities;
using System.Diagnostics;
using System.Reflection;

namespace Core.Logic
{
    public class JavascriptRunner
    {
        public static Grade RunScript(GradeKey key, List<Grade> grades)
        {           
            var engine = new Engine(cfg => cfg.AllowClr());
            Grade result = new Grade();

            try
            {
                engine
                    .SetValue("gradeKinds", key.UsedKinds.ToArray())
                    .SetValue("grades", grades.ToArray());

                // Das "console"-Objekt in der Jint-Engine erstellen
                ObjectInstance consoleObject = engine.Object.Construct(Arguments.Empty);
                consoleObject.FastAddProperty("log", new ClrFunctionInstance(engine, ConsoleLog), true, false, true);

                // Das "console"-Objekt der Jint-Engine hinzufügen
                engine.SetValue("console", consoleObject);
                engine.Execute(key.Calculation);

                // Get Return from Script
                var resultGrade = engine.GetValue("result");
                               
                result.Teacher = key.Teacher;
                result.Graduate = TypeConverter.ToInt32(resultGrade);
            }
            catch (Exception)
            {
                result.Teacher = null;
                result.Graduate = 0;
            }
            return result;
        }
        private static JsValue ConsoleLog(JsValue thisObject, object[] arguments)
        {
            Debug.WriteLine(string.Join(',', arguments));
            return JsValue.Undefined;
        }
    }
}
