using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;

public class Script
{
    public double Calculate(GradeKey key, List<Grade> grades)
    {
        int makCounter = 0;
        int testCounter = 0;
        int homeworkCounter = 0;

        int mak = 0;
        int test = 0;
        int homework = 0;

        foreach (var item in grades)
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

        Debugger.Break();
        
        return ((mak / makCounter) + (test / testCounter) + (homework / homeworkCounter)) / 3.0;
    }
}

