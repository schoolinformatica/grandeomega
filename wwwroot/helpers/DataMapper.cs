using System.Collections.Generic;
using System.Linq;
using kmeans;
using System;

namespace Mapper
{
    public static class DataMapper
    {
        public static List<GenericVector> ToAttempts(List<Dictionary<string, string>> data)
        {

            var students = data.GroupBy(x => x["student_id"]);
            var vectors = new List<GenericVector>();
            foreach (var student in students)
            {
                var suc = 0;
                var fail = 0;
                foreach (var attempt in student)
                {
                    if (attempt["sort"] == "succes")
                        suc++;
                    else
                        fail++;
                }
                vectors.Add(new GenericVector(new List<float> { student.Count(), suc / fail * 100 }));
            }
            return vectors;

        }

        public static List<GenericVector> ToAttemptsWithGrade(List<Dictionary<string, string>> data)
        {
            var students = data.GroupBy(x => x["student_id"]);
            var vectors = new List<GenericVector>();
            foreach (var student in students)
            {
                bool graded =false;
                float grade = 0;                
                var suc = 0;
                var fail = 0;
                foreach (var attempt in student)
                {
                    if (attempt["sort"] == "succes")
                        suc++;
                    else
                        fail++;

                    if(attempt.ContainsKey("grade")) {
                        var succes = float.TryParse(attempt["grade"], out grade);

                        if(succes) {

                            graded = true;
                        }
                    }
                }

                
                if(graded) {
                    vectors.Add(new GenericVector(new List<float> { student.Count(), suc / fail * 100, grade }));
                }
            }
            return vectors;

        }

    }


}
