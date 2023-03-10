using Core.Logic;
using Shared.Entities;
using Persistence;
using System.Threading.Tasks.Sources;
using Xunit.Sdk;

namespace CalculationTests
{
    /// <summary>
    /// Testing foreach ScripRunner
    /// </summary>
    [TestClass]
    public class CalculationTesting
    { 

        /// <summary>
        /// Test the LuaScriptRunner very simple 
        /// </summary>
        [TestMethod]
        public void LuaSimpleCalculation()
        {
            var gradeKinds = new List<GradeKind>
            {
                new GradeKind { Name = "MAK" },
                new GradeKind { Name = "TEST" },
                new GradeKind { Name = "HOMEWORK"}
            };

            var code = File.ReadAllText("test.lua");
            var key = new GradeKey { Name = "LuaTestKey", UsedKinds = gradeKinds, Calculation = code};

            var grades = new List<Grade>
            {
                new Grade { GradeKind = gradeKinds.Single(g => g.Name =="MAK") , Graduate = 5 },
                new Grade { GradeKind = gradeKinds.Single(g => g.Name =="MAK") , Graduate = 4 },
                new Grade { GradeKind = gradeKinds.Single(g => g.Name =="MAK") , Graduate = 3 },
                new Grade { GradeKind = gradeKinds.Single(g => g.Name =="MAK") , Graduate = 2 },
                new Grade { GradeKind = gradeKinds.Single(g => g.Name =="MAK") , Graduate = 1 },
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "TEST"), Graduate = 1},
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "TEST"), Graduate = 2},
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "TEST"), Graduate = 3},
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "TEST"), Graduate = 4},
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "TEST"), Graduate = 5},
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "HOMEWORK"), Graduate = 1},
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "HOMEWORK"), Graduate = 2},
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "HOMEWORK"), Graduate = 3},
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "HOMEWORK"), Graduate = 4},
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "HOMEWORK"), Graduate = 5},
        
            };

            var runner = new LuaScriptRunner();

            var result = runner.RunScript(key, grades);

            Assert.AreEqual(3, result.Graduate, "Calculation is right");

        }

        [TestMethod]
        public void JavaScriptTest()
        {
            //string fileName = "test.js";
            //string path = Path.Combine(Environment.CurrentDirectory, @"Data\", fileName);
            //string path = AppDomain.CurrentDomain.BaseDirectory + "\\CalculationTests\\test.js";

            var gradeKinds = new List<GradeKind>
            {
                new GradeKind { Name = "MAK" },
                new GradeKind { Name = "TEST" },
                new GradeKind { Name = "HOMEWORK"}
            };

            var code = File.ReadAllText("test.js");
            var key = new GradeKey { Name = "JavascriptTest", UsedKinds = gradeKinds, Calculation = code };

            var runner = new JavascriptRunner();
            var result = runner.RunScript(key);

            Assert.AreEqual(3, result.Graduate, "Calculation is right");
        }
    }
}