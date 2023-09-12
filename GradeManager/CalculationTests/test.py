import System.Array
import Core.Logic
import clr

clr.AddReference("MyNamespace")

def calculate():
    makGradesCount = 0
    makGrade = 0
    testGrade = 0
    testCount = 0
    hwCount = 0
    hwGrade = 0
    
    for grade in grades:

        if grade.GradeKind.Name == 'MAK':
            makGrade = makGrade + grade.Graduate
            makGradesCount = makGradesCount + 1
        
        elif grade.GradeKind.Name == 'TEST' :
            testCount = testCount + 1
            testGrade = testGrade + grade.Graduate
        
        elif grade.GradeKind.Name == 'HOMEWORK' :
            hwCount = hwCount + 1
            hwGrade = hwGrade + grade.Graduate

    mak = makGrade / makGradesCount
    test =  testGrade / testCount
    hw = hwGrade / hwCount


    result = (mak + test + hw) / 3 

    return result


graduate = calculate()