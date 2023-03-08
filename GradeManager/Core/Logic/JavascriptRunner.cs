using Jint.Runtime;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Logic
{
    public class JavascriptRunner
    {
        public Grade RunScript(GradeKey key)
        {
            if (key.Calculation == string.Empty || key.UsedKinds == null)
            {
                return null;
            }
            var engine = new Jint.Engine();
            Grade result = new Grade();

            try
            {
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
