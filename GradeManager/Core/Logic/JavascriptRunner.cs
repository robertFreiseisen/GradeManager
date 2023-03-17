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
                var gradeKinds = key.UsedKinds.ToArray();

                //var jsObjects = gradeKinds.Select(o => new {
                //    Name = o.Name
                //}).ToArray();
                var gradeKindz = Newtonsoft.Json.JsonConvert.SerializeObject(gradeKinds);
                engine.SetValue("gradeKinds", gradeKindz)
                      .SetValue("grades", grades.ToArray());

                //var returnFromScript = engine
                //    .Execute(key.Calculation)
                //    .GetValue("result");
                var returnFromScript = engine
                    .Execute(key.Calculation)
                    .GetValue("debug");
                var debug = TypeConverter.ToString(returnFromScript);
                result.Teacher = key.Teacher;
                //result.Graduate = TypeConverter.ToInt32(returnFromScript);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
    }
}
