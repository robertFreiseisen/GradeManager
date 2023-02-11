using Shared.Entities;
using Persistence;
using Microsoft.EntityFrameworkCore;

namespace Core.Logic
{
    /// <summary>
    /// Handels the diffrent types of Scripts and
    /// calls methods for calculation from each ScriptType
    /// </summary>
    public class GradeCalculator
    {
        private readonly ApplicationDbContext context;
        private readonly LuaScriptRunner luaScriptRunner;

        public GradeCalculator(ApplicationDbContext context, LuaScriptRunner luaScriptRunner)
        {
            this.context = context;
            this.luaScriptRunner = luaScriptRunner;
        }

        public async Task<List<Grade>> CalculateKeysForClassAndSubject(int schoolClassId, int subject)
        {
            var grades = await context.Grades
                .Include(g => g.Subject)
                .Include(g => g.Student)
                .Where(g => g.SubjectId == subject && g.Student!.SchoolClassId == schoolClassId).ToListAsync();

            if (grades == null || grades.Count() == 0)
            {
                return null;
            }

            var g1 = grades.First();
            var key = await context.GradeKeys.SingleOrDefaultAsync(k => k.SubjectId == subject && k.TeacherId == g1.TeacherId);

            if (key == null)
            {
                return null;
            }


            var result = new List<Grade>();

            var studentGrades = grades.GroupBy(g => g.Student).Select(g => this.RunScript(key, g.ToList(), g.Key!));
            return studentGrades.ToList();
        }

        /// Handels the script type
        private Grade RunScript(GradeKey gradeKey, List<Grade> grades, Student student)
        {
            var result = new Grade();
            result.Student = student;
            switch (gradeKey.ScriptType)
            {
                case ScriptType.None:
                    break;
                case ScriptType.Lua:
                    result = luaScriptRunner.RunScript(gradeKey, grades);
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
