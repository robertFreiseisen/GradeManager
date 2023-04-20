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
        public Grade RunScript(GradeKey key, List<Grade> grades)
        {
            if (key.Calculation == string.Empty || key.UsedKinds == null)
            {
                return null;
            }
            var engine = new Engine(options => options.DebugMode(true));
            Grade result = new Grade();

            try
            {
                //var jsObjects = gradeKinds.Select(o => new {
                //    Name = o.Name
                //}).ToArray();

                engine.SetValue("gradeKinds", key.UsedKinds.ToArray())
                      .SetValue("grades", grades.ToArray());
                      
                var returnFromScript = engine
                    .Execute(key.Calculation)
                    .GetValue("result");

                //engine.Invoke("debugger.start", 10);

                result.Teacher = key.Teacher;
                result.Graduate = TypeConverter.ToInt32(returnFromScript);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
        void HandleDebugInformation(DebugInformation info)
        {
            // Handle debugging information.
            //Console.WriteLine($"Line {info.Locals}, Column {info.Location.Start.Column}: {info.CurrentStatement}");
        }
    }
}
