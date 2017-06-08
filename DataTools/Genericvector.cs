using System;
using System.Linq;

namespace DataTools

{
    public class GenericVector
    {
        private readonly double[] _points;


        public int Size => _points.Length;
        public double BiggestPoint => _points.Max();


        public GenericVector(int size) => _points = new double[size];
        public GenericVector(params double[] args) => _points = args;


        public double[] ToArray() => _points;

        public double this[int x] => _points[x];


        public override string ToString()
        {
            return "[" + string.Join(", ", _points.Select(x => x.ToString()).ToArray()) + "]";
        }

        public Vector2 ToVector2(int indexOne = 0, int indexTwo = 1)
        {
            return new Vector2(_points[indexOne], _points[indexTwo]);
        }


        public static GenericVector Sum(GenericVector a, GenericVector b)
        {
            if (a.Size != b.Size)
                throw new Exception("GenericVector size of vectorToSum not equal to instance vector size");

            var aArray = a.ToArray();
            var bArray = b.ToArray();

            return new GenericVector(aArray.Zip(bArray, (x, y) => x + y).ToArray());
        }

        public static GenericVector Devide(GenericVector a, int devider)
        {
            return new GenericVector(a.ToArray().Select(x => x / devider).ToArray());
        }

        public static double Distance(GenericVector a, GenericVector b)
        {
            var aMinusBpoints = a._points.Select((t, i) => t - b._points[i]).ToArray();

            return Math.Sqrt(aMinusBpoints.Sum(item => Math.Pow(item, 2)));
        }
    }
}