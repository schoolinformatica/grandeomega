using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using webapp.wwwroot.models;

namespace webapp.wwwroot.scripts
{
    public static class Students
    {
        private const string WatchDirectory = "wwwroot/dbdump";
        private const string GradesFile = "wwwroot/grades.yaml";

        public static List<Student> StudentsGraded = new List<Student>();
        public static List<Student> StudentsUngraded = new List<Student>();

        public static void ParseData()
        {
            var data = new List<Dictionary<string, string>>();
            var studentLookup = new Dictionary<int, Student>();
            var gradess = new Dictionary<int, int>();
            var grades = Parser.Parse(GradesFile);
            var files = Directory.GetFiles(WatchDirectory);

            foreach (var file in files)
            {
                data.AddRange(Parser.Parse(file));
            }

            foreach (var grade in grades)
            {
                int score;
                if (int.TryParse(grade["grade"], out score))
                    gradess.Add(int.Parse(grade["student_id"]), score);
            }

            foreach (var attempt in data)
            {
                var att = new Attempt()
                {
                    Id = int.Parse(attempt["id"]),
                    TeachingUnitId = int.Parse(attempt["teaching_unit_id"]),
                    StudentId = int.Parse(attempt["student_id"]),
                    Success = attempt["sort"].Trim() == "success",
                    Date = DateTime.Parse(attempt["utc"].Substring(attempt["utc"].IndexOf('-') - 4))
                };

                if (!studentLookup.ContainsKey(att.StudentId))
                {
                    int Class;
                    var student = new Student()
                    {
                        Id = att.StudentId,
                        Grade = gradess.ContainsKey(att.StudentId) ? gradess[att.StudentId] : -1,
                        Class = int.TryParse(attempt["class"], out Class) ? Class : 0
                    };
                    student.Attempts.Add(att);
                    studentLookup.Add(student.Id, student);
                }
                else
                {
                    studentLookup[att.StudentId].Attempts.Add(att);
                }
            }
            StudentsGraded = studentLookup.Values.Where(x => x.Grade >= 0).ToList();
            StudentsUngraded = studentLookup.Values.Where(x => x.Grade < 0).ToList();
        }
    }
}