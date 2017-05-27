using System;
using System.Collections.Generic;
using System.Linq;
using Data;

namespace models
{
    public class Student
    {
        public int Id;
        public int Grade;
        public int Class;

        public List<Attempt> Attempts = new List<Attempt>();

        public void Filter()
        {
            var filteredAtt = new List<Attempt>();
            for (var i = 0; i < Attempts.Count; i++)
            {
                if (!filteredAtt.Any())
                {
                    filteredAtt.Add(Attempts[i]);
                    continue;
                }

                if (Math.Abs((filteredAtt.Last().Date - Attempts[i].Date).TotalSeconds) > 5)
                {
                    filteredAtt.Add(Attempts[i]);
                }

            }
            Attempts = filteredAtt;
        }

        public GenericVector ToGenericVector(params string[] args) {
            var v = new GenericVector();
//            for (int i = 0; i < args.Length; i++)
//            {
//                switch (args[i])
//                {
//                        case "Id":
//                            v.Add(Id);
//                        case "Grade":
//                }
//            }
            v.Add(Attempts.Count);
            v.Add(Grade);
            return v;
           
        }

    }
}