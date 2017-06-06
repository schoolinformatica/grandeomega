using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Matrix;

namespace DataTools.regression
{
    public class PolynomialRegression
    {
        private readonly double[][] _matrixPoints;
        private readonly double[] _matrixCoefficients;
        private readonly double[] _matrixYvalues;
        private readonly Vector2[] _data;

        private readonly Dictionary<int, double> _sigmaDegreeCache = new Dictionary<int, double>();

        private int PolynomialDegree { get; }

        public PolynomialRegression(IEnumerable<Vector2> data, int polynomialDegree)
        {
            _data = data.ToArray();
            _matrixPoints = MatrixUtils.MatrixCreate(polynomialDegree, polynomialDegree);
            _matrixCoefficients = new double[polynomialDegree];
            _matrixYvalues = new double[polynomialDegree];
            PolynomialDegree = polynomialDegree;

            ComputeMatrixPoints(polynomialDegree);
            ComputeMatrixYvalues(polynomialDegree);
            ComputerMatrixCoefficients();
        }

        public double PredictPoint(double x)
        {
            return ComputePolynomialPointY(x);
        }

        public GenericVector[] GetPolynomialPoints()
        {
            var polyPoints = new GenericVector[_data.Length];

            for (int i = 0; i < _data.Length; i++)
            {
                polyPoints[i] = new GenericVector(_data[i].X, ComputePolynomialPointY(_data[i].X));
            }

            return polyPoints;
        }

        private double ComputePolynomialPointY(double x)
        {
            var polyPoint = 0.0;

            for (int i = 0; i < PolynomialDegree; i++)
            {
                polyPoint += _matrixCoefficients[i] * Math.Pow(x, i);
            }

            return polyPoint;
        }

        private void ComputerMatrixCoefficients()
        {
            var inverseMatrixPoints = Matrix.MatrixUtils.MatrixInverse(_matrixPoints);

            for (int i = 0; i < inverseMatrixPoints.Length; i++)
            {
                var sum = 0.0;

                for (int j = 0; j < _matrixYvalues.Length; j++)
                {
                    sum += _matrixYvalues[j] * inverseMatrixPoints[i][j];
                }

                _matrixCoefficients[i] = sum;
            }
        }

        private void ComputeMatrixYvalues(int polynomialDegree)
        {
            for (var i = 0; i < polynomialDegree; i++)
            {
                _matrixYvalues[i] = ComputeSigmaPowerXY(i);
            }
        }

        private void ComputeMatrixPoints(int polynomialDegree)
        {
            for (var i = 0; i < polynomialDegree; i++)
            {
                for (var j = 0; j < polynomialDegree; j++)
                {
                    _matrixPoints[i][j] = ComputeSigmaPowerX(i + j);
                }
            }
        }

        private double ComputeSigmaPowerXY(int degree)
        {
            var sigmaPowerXY = 0.0;

            for (var i = 0; i < _data.Length; i++)
            {
                sigmaPowerXY += Math.Pow(_data[i].X, degree) * _data[i].Y;
            }

            return sigmaPowerXY;
        }

        private double ComputeSigmaPowerX(int degree)
        {
            if (!_sigmaDegreeCache.ContainsKey(degree))
            {
                var sum = 0.0;

                for (int i = 0; i < _data.Length; i++)
                {
                    sum += Math.Pow(_data[i].X, degree);
                }

                _sigmaDegreeCache[degree] = sum;
            }


            return _sigmaDegreeCache[degree];
        }
    }
}