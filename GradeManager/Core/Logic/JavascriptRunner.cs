using Jint;
using Jint.Runtime;
using Shared.Entities;

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
            var engine = new Engine();//(options => options.DebugMode());
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

                result.Teacher = key.Teacher;
                result.Graduate = TypeConverter.ToInt32(returnFromScript);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
    }
}
