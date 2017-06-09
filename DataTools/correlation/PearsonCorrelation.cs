using System;
using System.Collections.Generic;
using System.Linq;

namespace DataTools.correlation
{
    /******************************************************
     *
     * The Pearson Correlation Coefficient, is a linear 
     * correlation between two variables X and Y. It has a
     * value between -1 and 1, where -1 is maximum negative
     * correlation, 1 is maximum positive correlation and
     * 0 is no correlation at all.
     *
     ******************************************************/
    
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

            return (sumXtimesY - (sumX * sumY) / sampleLength) /
                   (Math.Sqrt((sumXSquared - Math.Pow(sumX, 2) / sampleLength) *
                              (sumYSquared - Math.Pow(sumY, 2) / sampleLength)));
        }
    }
}