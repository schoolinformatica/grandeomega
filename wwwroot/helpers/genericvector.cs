using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;

namespace Data

{
    public class GenericVector
    {
        //FIELDS
        public List<float> Points = new List<float>();


        //PROPERTIES
        public int Size => Points.Count;

        public float BiggestPoint => Points.Max();


        //CONSTRUCTORS

        public GenericVector(List<float> points)
        {
            Points = points;
        }

        public GenericVector(params float[] args)
        {
            Points = args.ToList();
        }

        public GenericVector(int size)
        {
            size.Times(() => Points.Add(0));
        }

        public GenericVector()
        {
        }


        //METHODS
        public void Add(float point)
        {
            Points.Add(point);
        }

        public GenericVector Sum(GenericVector vectorToSum)
        {
            if (Size != vectorToSum.Size)
                throw new Exception("GenericVector size of vectorToSum not equal to instance vector size");

            for (var i = 0; i < Points.Count; i++)
                Points[i] += vectorToSum.Points[i];

            return this;
        }

        public GenericVector Devide(int devider)
        {
            for (var i = 0; i < Size; i++)
                Points[i] /= devider;

            return this;
        }


        // public bool IsBiggerAs(GenericVector v)
        // {
        //     return Points.Where((p, i) => p > v.Points[i]).Count() > Points.Count;
        // }


        public override string ToString()
        {
            return "[" + string.Join(", ", Points.Select(x => x.ToString()).ToArray()) + "]";
        }


        public static double Distance(GenericVector a, GenericVector b)
        {
            var aMinusBpoints = a.Points.Select((t, i) => t - b.Points[i]).ToList();

            return Math.Sqrt(aMinusBpoints.Sum(item => Math.Pow(item, 2)));
        }
    }
}