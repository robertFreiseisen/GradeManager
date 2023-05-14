using Core.Logic;
using Shared.Entities;
using Persistence;
using System.Threading.Tasks.Sources;
using Xunit.Sdk;
using Jint;

namespace CalculationTests
{
    /// <summary>
    /// Testing foreach ScripRunner
    /// </summary>
    [TestClass]
    public class CalculationTesting
    {
        static List<GradeKind> gradeKinds = new List<GradeKind>
            {
                new GradeKind { Name = "MAK" },
                new GradeKind { Name = "TEST" },
                new GradeKind { Name = "HOMEWORK"}
            };
        List<Grade> grades = new List<Grade>
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

        /// <summary>
        /// Test the LuaScriptRunner very simple 
        /// </summary>
        [TestMethod]
        public void Lua_T01()
        {
            var code = File.ReadAllText("test.lua");
            var key = new GradeKey { Name = "LuaTestKey", UsedKinds = gradeKinds, Calculation = code };

            var runner = new LuaScriptRunner();

            var result = runner.RunScript(key, grades);

            Assert.AreEqual(3, result.Graduate, "Calculation is right");

        }

        [TestMethod]
        public void JavaScript_T01()
        {                      
            var code = File.ReadAllText("test.js");
            var key = new GradeKey { Name = "JavascriptTest", UsedKinds = gradeKinds, Calculation = code };

            var runner = new JavascriptRunner();
            var result = JavascriptRunner.RunScript(key, grades);

            Assert.AreEqual(3, result.Graduate, "Calculation is right");
        }
        [TestMethod]
        public void JavaScript_T02()
        {           
            List<Grade> secondGrades = new List<Grade>
            {
                new Grade { GradeKind = gradeKinds.Single(g => g.Name =="MAK") , Graduate = 1 },
                new Grade { GradeKind = gradeKinds.Single(g => g.Name =="MAK") , Graduate = 1 },
                new Grade { GradeKind = gradeKinds.Single(g => g.Name =="MAK") , Graduate = 1 },
                new Grade { GradeKind = gradeKinds.Single(g => g.Name =="MAK") , Graduate = 2 },
                new Grade { GradeKind = gradeKinds.Single(g => g.Name =="MAK") , Graduate = 1 },
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "TEST"), Graduate = 1},
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "TEST"), Graduate = 2},
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "TEST"), Graduate = 3},
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "TEST"), Graduate = 4},
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "TEST"), Graduate = 2},
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "HOMEWORK"), Graduate = 1},
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "HOMEWORK"), Graduate = 2},
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "HOMEWORK"), Graduate = 3},
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "HOMEWORK"), Graduate = 4},
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "HOMEWORK"), Graduate = 5},

            };

            var code = File.ReadAllText("test.js");
            var key = new GradeKey { Name = "JavascriptTest", UsedKinds = gradeKinds, Calculation = code };

            var runner = new JavascriptRunner();
            var result = JavascriptRunner.RunScript(key, secondGrades);

            Assert.AreEqual(2, result.Graduate, "Calculation is right");
        }
        //[TestMethod]
        //public void SimpleOutputTest()
        //{
        //    var gradeKinds = new List<GradeKind>
        //    {
        //        new GradeKind { Name = "MAK" },
        //        new GradeKind { Name = "TEST" },
        //        new GradeKind { Name = "HOMEWORK"}
        //    };
        //    var grades = new List<Grade>
        //    {
        //        new Grade { GradeKind = gradeKinds.Single(g => g.Name =="MAK") , Graduate = 1 },
        //        new Grade { GradeKind = gradeKinds.Single(g => g.Name =="MAK") , Graduate = 1 },
        //        new Grade { GradeKind = gradeKinds.Single(g => g.Name =="MAK") , Graduate = 3 },
        //        new Grade { GradeKind = gradeKinds.Single(g => g.Name =="MAK") , Graduate = 2 },
        //        new Grade { GradeKind = gradeKinds.Single(g => g.Name =="MAK") , Graduate = 1 },
        //        new Grade { GradeKind = gradeKinds.Single(g => g.Name == "TEST"), Graduate = 1},
        //        new Grade { GradeKind = gradeKinds.Single(g => g.Name == "TEST"), Graduate = 2},
        //        new Grade { GradeKind = gradeKinds.Single(g => g.Name == "TEST"), Graduate = 3},
        //        new Grade { GradeKind = gradeKinds.Single(g => g.Name == "TEST"), Graduate = 1},
        //        new Grade { GradeKind = gradeKinds.Single(g => g.Name == "TEST"), Graduate = 1},
        //        new Grade { GradeKind = gradeKinds.Single(g => g.Name == "HOMEWORK"), Graduate = 1},
        //        new Grade { GradeKind = gradeKinds.Single(g => g.Name == "HOMEWORK"), Graduate = 2},
        //        new Grade { GradeKind = gradeKinds.Single(g => g.Name == "HOMEWORK"), Graduate = 3},
        //        new Grade { GradeKind = gradeKinds.Single(g => g.Name == "HOMEWORK"), Graduate = 4},
        //        new Grade { GradeKind = gradeKinds.Single(g => g.Name == "HOMEWORK"), Graduate = 5},

        //    };

        //    var code = File.ReadAllText("test.js");
        //    var key = new GradeKey { Name = "JavascriptTest", UsedKinds = gradeKinds, Calculation = code };

        //    var runner = new JavascriptRunner();
        //    var result = JavascriptRunner.RunScript(key, grades);           

        //    Assert.AreEqual("8", result);
        //}

        [TestMethod]
        public void CsScript_T01()
        {           
            var code = File.ReadAllText("test.cs");
            var key = new GradeKey { Name = "CsScriptTest", UsedKinds = gradeKinds, Calculation = code };

            var result = CsScriptRunner.RunScript(key, grades);

            Assert.AreEqual(3, result.Graduate, "Calculation is right");
        }

        [TestMethod]
        public void CsScript_T02()
        {
            List<Grade> secondGrades = new List<Grade>
            {
                new Grade { GradeKind = gradeKinds.Single(g => g.Name =="MAK") , Graduate = 1 },
                new Grade { GradeKind = gradeKinds.Single(g => g.Name =="MAK") , Graduate = 1 },
                new Grade { GradeKind = gradeKinds.Single(g => g.Name =="MAK") , Graduate = 1 },
                new Grade { GradeKind = gradeKinds.Single(g => g.Name =="MAK") , Graduate = 2 },
                new Grade { GradeKind = gradeKinds.Single(g => g.Name =="MAK") , Graduate = 1 },
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "TEST"), Graduate = 1},
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "TEST"), Graduate = 2},
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "TEST"), Graduate = 3},
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "TEST"), Graduate = 4},
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "TEST"), Graduate = 2},
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "HOMEWORK"), Graduate = 1},
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "HOMEWORK"), Graduate = 2},
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "HOMEWORK"), Graduate = 3},
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "HOMEWORK"), Graduate = 4},
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "HOMEWORK"), Graduate = 5},

            };

            var code = File.ReadAllText("test.cs");
            var key = new GradeKey { Name = "CsScriptTest", UsedKinds = gradeKinds, Calculation = code };

            var result = CsScriptRunner.RunScript(key, secondGrades);

            Assert.AreEqual(2, result.Graduate, "Calculation is right");
        }
        [TestMethod]
        public void CsScript_T03()
        {
            List<Grade> secondGrades = new List<Grade>
            {
                new Grade { GradeKind = gradeKinds.Single(g => g.Name =="MAK") , Graduate = 2 },
                new Grade { GradeKind = gradeKinds.Single(g => g.Name =="MAK") , Graduate = 1 },
                new Grade { GradeKind = gradeKinds.Single(g => g.Name =="MAK") , Graduate = 1 },
                new Grade { GradeKind = gradeKinds.Single(g => g.Name =="MAK") , Graduate = 2 },
                new Grade { GradeKind = gradeKinds.Single(g => g.Name =="MAK") , Graduate = 1 },
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "TEST"), Graduate = 1},
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "TEST"), Graduate = 2},
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "TEST"), Graduate = 3},
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "TEST"), Graduate = 1},
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "TEST"), Graduate = 2},
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "HOMEWORK"), Graduate = 1},
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "HOMEWORK"), Graduate = 2},
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "HOMEWORK"), Graduate = 3},
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "HOMEWORK"), Graduate = 4},
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "HOMEWORK"), Graduate = 4},
            };

            var code = File.ReadAllText("test.cs");
            var key = new GradeKey { Name = "CsScriptTest", UsedKinds = gradeKinds, Calculation = code };

            var result = CsScriptRunner.RunScript(key, secondGrades);

            Assert.AreEqual(1, result.Graduate, "Calculation is right");
        }
        [TestMethod]
        public void CsScript_T04()
        {
            List<Grade> secondGrades = new List<Grade>
            {
                new Grade { GradeKind = gradeKinds.Single(g => g.Name =="MAK") , Graduate = 2 },
                new Grade { GradeKind = gradeKinds.Single(g => g.Name =="MAK") , Graduate = 1 },
                new Grade { GradeKind = gradeKinds.Single(g => g.Name =="MAK") , Graduate = 1 },
                new Grade { GradeKind = gradeKinds.Single(g => g.Name =="MAK") , Graduate = 2 },
                new Grade { GradeKind = gradeKinds.Single(g => g.Name =="MAK") , Graduate = 1 },
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "TEST"), Graduate = 1},
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "TEST"), Graduate = 2},
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "TEST"), Graduate = 3},
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "TEST"), Graduate = 1},
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "TEST"), Graduate = 2},
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "HOMEWORK"), Graduate = 1},
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "HOMEWORK"), Graduate = 3},
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "HOMEWORK"), Graduate = 3},
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "HOMEWORK"), Graduate = 4},
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "HOMEWORK"), Graduate = 4},
            };

            var code = File.ReadAllText("test.cs");
            var key = new GradeKey { Name = "CsScriptTest", UsedKinds = gradeKinds, Calculation = code };

            var result = CsScriptRunner.RunScript(key, secondGrades);

            Assert.AreEqual(2, result.Graduate, "Calculation is right");
        }
        [TestMethod]
        public void CsScriptMicrosoft_T01()
        {
            var code = File.ReadAllText("testScript.cs");
            var key = new GradeKey { Name = "CsScriptMicrosoftTest", UsedKinds = gradeKinds, Calculation = code };

            var result = CsScriptMicrosoftRunner.RunScriptAsync(key, grades);

            Assert.AreEqual(3, result.Result.Graduate, "Calculation is right");
        }
        [TestMethod]
        public void CsScriptMicrosoft_T02()
        {
            List<Grade> secondGrades = new List<Grade>
            {
                new Grade { GradeKind = gradeKinds.Single(g => g.Name =="MAK") , Graduate = 1 },
                new Grade { GradeKind = gradeKinds.Single(g => g.Name =="MAK") , Graduate = 1 },
                new Grade { GradeKind = gradeKinds.Single(g => g.Name =="MAK") , Graduate = 1 },
                new Grade { GradeKind = gradeKinds.Single(g => g.Name =="MAK") , Graduate = 2 },
                new Grade { GradeKind = gradeKinds.Single(g => g.Name =="MAK") , Graduate = 1 },
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "TEST"), Graduate = 1},
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "TEST"), Graduate = 2},
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "TEST"), Graduate = 3},
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "TEST"), Graduate = 4},
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "TEST"), Graduate = 2},
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "HOMEWORK"), Graduate = 1},
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "HOMEWORK"), Graduate = 2},
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "HOMEWORK"), Graduate = 3},
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "HOMEWORK"), Graduate = 4},
                new Grade { GradeKind = gradeKinds.Single(g => g.Name == "HOMEWORK"), Graduate = 5},

            };
            var code = File.ReadAllText("testScript.cs");
            var key = new GradeKey { Name = "CsScriptMicrosoftTest", UsedKinds = gradeKinds, Calculation = code };

            var result = CsScriptMicrosoftRunner.RunScriptAsync(key, secondGrades);

            Assert.AreEqual(2, result.Result.Graduate, "Calculation is right");
        }
    }
}