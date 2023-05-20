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

<<<<<<< Updated upstream
                // Das "console"-Objekt in der Jint-Engine erstellen
                ObjectInstance consoleObject = engine.Object.Construct(Arguments.Empty);
                consoleObject.FastAddProperty("log", new ClrFunctionInstance(engine, ConsoleLog), true, false, true);

                // Das "console"-Objekt der Jint-Engine hinzufügen
                engine.SetValue("console", consoleObject);
                engine.Execute(key.Calculation);
=======
                // Definiere die console.log-Funktion im JavaScript-Code
                engine.Execute(
                    @"
                                var console = {
                                    log: function() {
                                        consoleOutput.push(Array.from(arguments).join(' '));
                                    }
                                };
                            "
                );

                var gradeKindsList = JsonConvert.SerializeObject(key.UsedKinds);
                var gradesList = JsonConvert.SerializeObject(grades);

                engine.SetVariableValue("gradeKindsList", gradeKindsList);
                engine.SetVariableValue("gradesList", gradesList);

                if (key.Calculation != null)
                {
                    engine.Execute(key.Calculation);
                }

                //Die Ausgabe der console.log-Anweisungen als JSON-String
                string jsonOutput = engine.Evaluate<string>("JSON.stringify(consoleOutput)");

                // Konvertiere den JSON-String in eine Liste von strings
                if (jsonOutput != null)
                {
                    logs = JsonConvert.DeserializeObject<List<string>>(jsonOutput);

                    if (logs != null)
                    {
                        DisplayOutput(logs);
                    }
                }
>>>>>>> Stashed changes

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
<<<<<<< Updated upstream
        private static JsValue ConsoleLog(JsValue thisObject, object[] arguments)
=======

        private static void DisplayOutput(List<string> logs)
>>>>>>> Stashed changes
        {
            Debug.WriteLine(string.Join(',', arguments));
            return JsValue.Undefined;
        }
    }
}
