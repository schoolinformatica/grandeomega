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

        public GenericVector ToGenericVector(params Stud[] args) {
            var v = new GenericVector();
            
            for (int i = 0; i < args.Length; i++)
               v.Add(GetValueOf(args[i]));
            
            return v;
        }

        private float GetValueOf(Stud key)
        {
            switch (key)
            {
                   case Stud.Id:
                       return Id;
                       
                   case Stud.Grade:
                       return Grade;
                       
                   case Stud.Class:
                       return Class;
                       
                   case Stud.Attempts:
                       return Attempts.Count;
                       
                   case Stud.Fails:
                       return Attempts.Count(x => !x.Success);
                       
                   case Stud.Succeeds:
                       return Attempts.Count(x => x.Success);
                       
                   case Stud.SuccessRatio:
                       return Attempts.Count(x => x.Success) / Attempts.Count();
                       
                   case  Stud.FailRatio:
                       return Attempts.Count(x => !x.Success) / Attempts.Count();
                   
                   default:
                       throw new Exception("Enumeration not implemented yet");
            }
        }

    }

    public enum Stud
    {
        Id,
        Grade,
        Class,
        Attempts,
        Fails,
        Succeeds,
        SuccessRatio,
        FailRatio
    }
}