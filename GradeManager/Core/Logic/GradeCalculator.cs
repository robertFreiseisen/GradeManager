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
        private readonly JavascriptRunner jsRunner;
        private readonly CsScriptMicrosoftRunner csScriptRunner;
        private readonly LuaScriptRunner luaScriptRunner;

        public GradeCalculator(ApplicationDbContext context, 
                               LuaScriptRunner luaScriptRunner, 
                               JavascriptRunner jsRunner,
                               CsScriptMicrosoftRunner csRunner)
        {
            this.context = context;
            this.luaScriptRunner = luaScriptRunner;
            this.csScriptRunner = csRunner;
            this.jsRunner = jsRunner;
        }

        public async Task<List<Grade>> CalculateKeysForClassAndSubject(int schoolClassId, int subject)
        {
            var grades = await context.Grades
                .Include(g => g.Subject)
                .Include(g => g.Student)
                .Where(g => g.SubjectId == subject && g.Student!.SchoolClassId == schoolClassId)
                .ToListAsync();

            if (grades == null || grades.Count() == 0)
            {
                return new List<Grade>();
            }

            var g1 = grades.First();
            var key = await context.GradeKeys
                .SingleOrDefaultAsync(k => k.SubjectId == subject && k.TeacherId == g1.TeacherId);

            if(key == null)
            {
                throw new NullReferenceException("No Key found");
            }
                
            var result = new List<Grade>();

            foreach (var item in grades.DistinctBy(g => g.StudentId).Select(g => g.StudentId))
            {
                var student = context.Students.Single(s => s.Id == item);
                var gradesForCalc =  grades.Where(g => g.StudentId == student.Id).ToList();
                var gradeForStudent = await this.RunScriptAsync(key, 
                                                   gradesForCalc, 
                                                   student);
                gradeForStudent.TeacherId = key.TeacherId;
                gradeForStudent.StudentId = item;
                gradeForStudent.SubjectId = key.SubjectId;
                gradeForStudent.GradeKindId = 1;
                result.Add(gradeForStudent);
            }

            return result;
        }

        /// Handels the script type
        private async Task<Grade> RunScriptAsync(GradeKey gradeKey, List<Grade> grades, Student student)
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
                    result = jsRunner.RunScript(gradeKey, grades);
                    break;
                case ScriptType.CSharpScript:
                    result = await csScriptRunner.RunScriptAsync(gradeKey, grades);
                    break;
                default:
                    result = null;
                    break;
            }

            if (result == null)
            {
                throw new Exception("Erorr in Calculation");
            }

            result.Student = student;

            result.Note = $"{student.Name} : {DateTime.Now}";

            return result;
        }


    }
}
