using YamlParser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using models;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authentication;

public static class Students
{
    public static IEnumerable<IGrouping<string, Dictionary<string, string>>> studentAttempts;
    public static List<Student> students = new List<Student>();
    public static List<string> properties = new List<string>();

    public static void parseData()
    {
        var WatchDir = "wwwroot/dbdump";
        var files = Directory.GetFiles(WatchDir);
        var parser = new Parser();
        var data = new List<Dictionary<string, string>>();
        foreach (var file in files)
        {
            data.AddRange(parser.Parse(file));
        }
        var grades = parser.Parse("wwwroot/grades.yaml");

        Dictionary<int, Student> temp = new Dictionary<int, Student>();
        Dictionary<int, int> gradess = new Dictionary<int, int>();

        foreach(var grade in grades) {
            int score;
            var isInt = int.TryParse(grade["grade"], out score);
            if(isInt)
                gradess.Add(int.Parse(grade["student_id"]), score);
        }

        // Regex rgx = new Regex("[^a-zA-Z0-9 -]");
        properties = data.First().Keys.ToList();
        foreach (var attempt in data)
        {
            var att = new Attempt() {
                Id = int.Parse(attempt["id"]),
                TeachingUnitId = int.Parse(attempt["teaching_unit_id"]),
                StudentId = int.Parse(attempt["student_id"]),
                Success = attempt["sort"] == "success" ? true : false,
                // stepID = int.Parse(attempt["step_id"]), never filled in
                Date = DateTime.Parse(attempt["utc"].Substring(attempt["utc"].IndexOf('-') - 4))
            };

            if(!temp.ContainsKey(att.StudentId)) {
                var student = new Student() {
                    Id = att.StudentId,
                    Grade = gradess.ContainsKey(att.StudentId) ? gradess[att.StudentId] : -1
                };
                student.Attempts.Add(att);
                temp.Add(student.Id, student);
            } else {
                temp[att.StudentId].Attempts.Add(att);
            }

            
        }


        students = temp.Values.ToList();

    }

    // public static List<GenericVector> PickDataPerStudent(params string[] args)
    // {
    //     var list = new List<GenericVector>();
    //     foreach (var student in studentAttempts)
    //     {
    //         foreach (var attempt in student)
    //         {
    //             var point = new GenericVector();
    //             foreach (var arg in args)
    //             {
    //                 if(!attempt.ContainsKey(arg))
    //                     break;
    //                 point.Add(float.Parse(attempt[arg]));
    //             }
    //             list.Add(point);
    //         }
    //     }
    //     return list;
    // }

    public static string ToJSList(params string[] args)
    {


        var result = "[";
        foreach (var student in studentAttempts)
        {
            Console.WriteLine(student.First()["utc"].Substring(student.First()["utc"].IndexOf('-') - 4));
            var date = DateTime.Parse(student.First()["utc"].Substring(student.First()["utc"].IndexOf('-') - 4));
            if (!student.First().ContainsKey("grade")) continue;

            var suc = 0.0;
            var fail = 0.0;
            foreach (var attempt in student)
            {
                if (attempt["sort"] == "success")
                {
                    suc++;
                }
                else
                {
                    fail++;
                }
            }
            result += "[" + fail + "," + student.First()["grade"] + "],";
        }
        result += "]";

        return result;
    }
}