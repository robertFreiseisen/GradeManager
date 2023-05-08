using Jint;
using Jint.Runtime;
using Jint.Runtime.Debugger;
using Shared.Entities;
using System.Diagnostics;
using System.Reflection;

namespace Core.Logic
{
    public class JavascriptRunner
    {
        public static Grade RunScript(GradeKey key, List<Grade> grades)
        {
            if (key.Calculation == string.Empty || key.UsedKinds == null)
            {
                return null;
            }
            var engine = new Engine(cfg => cfg.AllowClr());
            Grade result = new Grade();
            var logs = string.Empty;

            try
            {
                engine
                    .SetValue("log", new Action<object>(Console.WriteLine))
                    .SetValue("gradeKinds", key.UsedKinds.ToArray())
                     .SetValue("grades", grades.ToArray());

                engine.Execute(key.Calculation);
                var resultGrade = engine.GetValue("result");
                //logs = TypeConverter.ToString(engine.GetValue("log"));

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
    }
}
