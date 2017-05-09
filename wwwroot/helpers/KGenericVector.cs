using System;
using Data;
using Helpers;

namespace Clustering
{
    public class KGenericVector : GenericVector
    {
        //PROPERTIES
//        public int Cluster;
        public int? Cluster { get; set; }
        public bool Visited { get; set; }
        public bool Noise { get; set; }



        //CONSTRUCTORS
        public KGenericVector(int size) { size.Times(() => Points.Add(0)); }
        public KGenericVector(GenericVector v) { Points = v.Points; }



        //METHODS
        public new KGenericVector Sum(GenericVector vectorToSum)
        {
            if (Size != vectorToSum.Size)
                throw new Exception("GenericVector size of vectorToSum not equal to instance vector size");

            for (var i = 0; i < Points.Count; i++)
                Points[i] += vectorToSum.Points[i];

            return this;
        }

        public new KGenericVector Devide(int devider)
        {
            for (var i = 0; i < Size; i++)
                Points[i] /= devider;

            return this;
        }

    }
}