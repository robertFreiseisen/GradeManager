int makCounter = 0;
int testCounter = 0;
int homeworkCounter = 0;

int mak = 0;
int test = 0;
int homework = 0;

foreach (var item in Grades)
{
    if (item.GradeKind.Name == "MAK")
    {
        mak += item.Graduate;
        makCounter++;
    }
    if (item.GradeKind.Name == "TEST")
    {
        test += item.Graduate;
        testCounter++;
    }
    if (item.GradeKind.Name == "HOMEWORK")
    {
        homework += item.Graduate;
        homeworkCounter++;
    }
}

double result = ((mak / makCounter) + (test / testCounter) + (homework / homeworkCounter)) / 3.0;