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

        public GenericVector ToGenericVector() {
            var v = new GenericVector();
            v.Add(Attempts.Count);
            v.Add(Grade);
            return v;
           
        }

    }
}