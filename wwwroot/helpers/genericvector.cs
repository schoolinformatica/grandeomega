using System;
using System.Collections.Generic;
using System.Linq;

namespace kmeans
{
    public class GenericVector
    {
        public int Cluster { get; set; }
        //list of floats that's creating the GenericVector
        public List<float> Points = new List<float>();

        //CONSTRUCTORS
        //Creates a new GenericVector with the points given
        public GenericVector(List<float> points)
        {
            Points = points;
        }

        //Creates a new GeneriVector with the points as long as the size given
        public GenericVector(int size)
        {
            size.Times(() => Points.Add(0));
        }

        public GenericVector()
        {
        }

        //GenericVector METHODS
        public void Add(float point)
        {
            Points.Add(point);
        }

        public int Size
        {
            get { return Points.Count; }
        }

        public GenericVector Sum(GenericVector vectorToSum)
        {
            if (Size != vectorToSum.Size)
                throw new Exception("GenericVector size of vectorToSum not equal to instance vector size");

            for (var i = 0; i < Points.Count; i++)
            {
                Points[i] += vectorToSum.Points[i];
            }
            return this;
        }

        public GenericVector Devide(int devider)
        {
            for (int i = 0; i < Size; i++)
            {
                Points[i] /= devider;
            }
            return this;
        }

        //Override ToString-Method to show the content of the GenericVector
        public override string ToString()
        {
            return string.Join(", ", Points.Select(x => x.ToString()).ToArray());
        }

        public bool IsBiggerAs(GenericVector v)
        {
            return Points.Where((p, i) => p > v.Points[i]).Count() > Points.Count;
        }

        public float BiggestPoint()
        {
            return Points.Max();
        }

        public static double Distance(GenericVector a, GenericVector b)
        {
            var aMinusBpoints = a.Points.Select((t, i) => t - b.Points[i]).ToList();

            return Math.Sqrt(aMinusBpoints.Sum(item => Math.Pow(item, 2)));
        }
    }
}