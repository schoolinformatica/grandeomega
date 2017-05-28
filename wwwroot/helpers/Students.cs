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

        var temp = new Dictionary<int, Student>();
        var gradess = new Dictionary<int, int>();

        foreach(var grade in grades) {
            int score;
            var isInt = int.TryParse(grade["grade"], out score);
            if(isInt)
                gradess.Add(int.Parse(grade["student_id"]), score);
        }

        properties = data.First().Keys.ToList();
        foreach (var attempt in data)
        {
            int Class;
            
            var att = new Attempt() {
                Id = int.Parse(attempt["id"]),
                TeachingUnitId = int.Parse(attempt["teaching_unit_id"]),
                StudentId = int.Parse(attempt["student_id"]),
                Success = attempt["sort"] == "success" ? true : false,
                Date = DateTime.Parse(attempt["utc"].Substring(attempt["utc"].IndexOf('-') - 4))
            };
            
            if(!temp.ContainsKey(att.StudentId)) {
                var student = new Student() {
                    Id = att.StudentId,
                    Grade = gradess.ContainsKey(att.StudentId) ? gradess[att.StudentId] : -1,
                    Class = int.TryParse(attempt["class"], out Class) ? Class : 0
                };
                student.Attempts.Add(att);
                temp.Add(student.Id, student);
            } else {
                temp[att.StudentId].Attempts.Add(att);
            }

            
        }
        students = temp.Values.ToList();
    }

    
}