function calculate()

    local makGradesCount = 0
    local makGrade = 0
    local testGrade = 0
    local testCount = 0
    local hwCount = 0
    local hwGrade = 0

    for countGrade = 0, grades.Count - 1 do 
        if grades[countGrade].GradeKind.Name == 'MAK' then
            makGrade = makGrade + grades[countGrade].Graduate
            makGradesCount = makGradesCount + 1
        elseif grades[countGrade].GradeKind.Name == 'TEST' then
            testCount = testCount + 1
            testGrade = testGrade + grades[countGrade].Graduate
        elseif grades[countGrade].GradeKind.Name == 'HOMEWORK' then
            hwCount = hwCount + 1
            hwGrade = hwGrade + grades[countGrade].Graduate
        end
    end

    return ((makGrade / makGradesCount) + ( testGrade / testCount ) + (hwGrade / hwCount)) / 3     
end