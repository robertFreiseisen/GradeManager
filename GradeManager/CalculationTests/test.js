var grades = JSON.parse(gradesList);
var gradeKinds = JSON.parse(gradeKindsList);

function calculate() {
    let makCounter = 0;
    let testCounter = 0;
    let homeworkCounter = 0;

    let mak = 0;
    let test = 0;
    let homework = 0;

    for (let i = 0; i < grades.length; i++) {

        if (grades[i].GradeKind.Name == 'MAK') {
            mak += grades[i].Graduate;
            makCounter++;
        }
        if (grades[i].GradeKind.Name == 'TEST') {
            test += grades[i].Graduate;
            testCounter++;
        }
        if (grades[i].GradeKind.Name == 'HOMEWORK') {
            homework += grades[i].Graduate;
            homeworkCounter++;
        }
    }
    return ((mak / makCounter) + (test / testCounter) + (homework / homeworkCounter)) / 3;
}
var result = calculate();
console.log('Wert: ' +grades[1].GradeKind.Name);