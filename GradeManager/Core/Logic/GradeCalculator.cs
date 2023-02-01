using Core.Contracts;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Logic
{
    /// <summary>
    /// Handels the diffrent types of Scripts and
    /// calls methods for calculation from each ScriptType
    /// </summary>
    public class GradeCalculator
    {
        private readonly IUnitOfWork uow;
        private readonly LuaScriptRunner luaScriptRunner;

        public GradeCalculator(IUnitOfWork uow, LuaScriptRunner luaScriptRunner)
        {
            this.uow = uow;
            this.luaScriptRunner = luaScriptRunner;
        }

        public async Task<List<Grade>?> CalculateKeysForClassAndSubject(int schoolClassId, int subject, IUnitOfWork uow)
        {
            var grades = await (uow.GradeRepository.GetByClassAndSubjectAsync(schoolClassId, subject));

            if (grades == null || grades.Count() == 0)
            {
                return null;
            }

            var g1 = grades.First();
            var key = await uow.GradeKeyRepository.GetByTeacherAndSubjectAsync(g1.TeacherId, g1.SubjectId);

            if (key == null)
            {
                return null;
            }

            return grades.Select(item => RunScript(key)).ToList();
        }

        private Grade RunScript(GradeKey gradeKey)
        {
            var result = new Grade();
            switch (gradeKey.ScriptType)
            {
                case ScriptType.None:
                    break;
                case ScriptType.Lua:
                    result = luaScriptRunner.RunScript(gradeKey);
                    break;
                case ScriptType.Python:
                    break;
                case ScriptType.JavaScript:
                    break;
                case ScriptType.CSharpScript:
                    break;
                default:
                    break;
            }

            return result;
        }

    }
}
