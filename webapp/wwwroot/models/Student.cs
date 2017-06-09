using System;
using System.Collections.Generic;
using System.Linq;
using DataTools;

namespace webapp.wwwroot.models
{
    //TODO: Make typesafe enum for the Student possibilities
    public class Student
    {
        public int Id;
        public int Grade;
        public int Class;

        public List<Attempt> Attempts = new List<Attempt>();

        public void Filter()
        {
            var filteredAtt = new List<Attempt>();

            foreach (var attempt in Attempts)
            {
                if (!filteredAtt.Any())
                {
                    filteredAtt.Add(attempt);
                    continue;
                }

                if (Math.Abs((filteredAtt.Last().Date - attempt.Date).TotalSeconds) > 5)
                    filteredAtt.Add(attempt);
            }

            Attempts = filteredAtt;
        }

        public GenericVector ToGenericVector(params Stud[] args)
        {
            var v = new double[args.Length];

            for (var i = 0; i < args.Length; i++)
                v[i] = GetValueOf(args[i]);

            return new GenericVector(v);
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
                    return Attempts.Count(x => x.Success) / (float) Attempts.Count();

                case Stud.FailRatio:
                    return Attempts.Count(x => !x.Success) / (float) Attempts.Count();

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