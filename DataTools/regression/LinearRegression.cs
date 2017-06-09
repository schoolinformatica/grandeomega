using System;
using System.Collections.Generic;
using System.Linq;

namespace DataTools.regression
{
    /******************************************************
     *
     * Linear Regression models the relationship between a 
     * dependent variable Y and a more independent variable
     * X. The regression line is represented by the formula
     * y = m * x + b, where m the slope and b the 
     * Y-intercept.
     *
     ******************************************************/
    
    public class LinearRegression : Regression
    {
        private readonly IEnumerable<Vector2> _data;
        
        private double MeanX { get; }
        private double MeanY { get; } 
        
        public double Slope => GetSlope();
        public double YIntercept => MeanY - (Slope * MeanX);
            
        public LinearRegression(IEnumerable<Vector2> data)
        {
            _data = data;
            MeanX = Mean(_data.Select(vector => vector.X));
            MeanY = Mean(_data.Select(vector => vector.Y));
        }
        
        public override IEnumerable<GenericVector> GetRegressionLine()
        {
            return GetLinearRegressionLine();
        }

        private IEnumerable<GenericVector> GetLinearRegressionLine()
        {
            var xL = _data.Select(vector => vector.X).Min();
            var yL = (float) (Slope * xL + YIntercept);
            var xR = _data.Select(vector => vector.X).Max();
            var yR = (float) (Slope * xR + YIntercept);
            var vectors = new List<GenericVector> {new GenericVector(xL, yL), new GenericVector(xR, yR)};

            return vectors;
        }
        
        private double GetSlope()
        {
            var nominator = 0.0;
            var denominator = 0.0;

            foreach (var vector in _data)
            {
                nominator += (vector.X - MeanX) * (vector.Y - MeanY);
                denominator += Math.Pow(vector.X - MeanX, 2);
            }

            return nominator / denominator;
        }
        
        private static double Mean(IEnumerable<double> sample) => sample.Sum() / sample.Count();

    }
}