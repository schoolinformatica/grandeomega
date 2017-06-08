using System;
using System.Collections.Generic;
using System.Linq;

namespace DataTools.correlation
{
    public class PearsonCorrelation : Correlation
    {
        public PearsonCorrelation(IEnumerable<Vector2> data) : base(data)
        {
        }

        public override double GetCorrelationCoefficient()
        {
            return PearsonCofficient();
        }
        
        private double PearsonCofficient()
        {
            var sampleLength = Data.Count();
            var sumXtimesY = 0.0;
            var sumX = 0.0;
            var sumY = 0.0;
            var sumXSquared = 0.0;
            var sumYSquared = 0.0;

            foreach (var vector in Data)
            {
                sumX += vector.X;
                sumY += vector.Y;
                sumXtimesY += vector.X * vector.Y;
                sumXSquared += vector.X * vector.X;
                sumYSquared += vector.Y * vector.Y;
            }


//            for (var i = 0; i < sampleLength; i++)
//            {
//                sumAB += _sampleA.ElementAt(i) * _sampleB.ElementAt(i);
//                sumX += _sampleA.ElementAt(i);
//                sumY += _sampleB.ElementAt(i);
//                sumXSquared += Math.Pow(_sampleA.ElementAt(i), 2);
//                sumYSquared += Math.Pow(_sampleB.ElementAt(i), 2);
//            }

            return (sumXtimesY - (sumX * sumY) / sampleLength) /
                   (Math.Sqrt((sumXSquared - Math.Pow(sumX, 2) / sampleLength) *
                              (sumYSquared - Math.Pow(sumY, 2) / sampleLength)));
        }
    }
}