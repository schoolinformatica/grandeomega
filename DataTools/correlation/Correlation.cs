using System.Collections.Generic;
using System;
using System.Linq;
using Data;

namespace Regression
{
    public class SimpleRegression

    {
        private readonly List<double> _sampleA = new List<double>();
        private readonly List<double> _sampleB = new List<double>();

        public double PearsonCorrelation => PearsonCofficient();
        public double SpearmanCorrelation => SpearmanCofficient();

        public SimpleRegression(IEnumerable<GenericVector> data)
        {
            if (data.First().Size != 2)
                throw new NotSupportedException("Correlation is only supported with two dimensions yet");

            foreach (var vector in data)
            {
                _sampleA.Add(vector[0]);
                _sampleB.Add(vector[1]);
            }
        }

        private double PearsonCofficient()
        {
            var sampleLength = _sampleA.Count();
            var sumAB = 0.0;
            var sumX = 0.0;
            var sumY = 0.0;
            var sumXSquared = 0.0;
            var sumYSquared = 0.0;


            for (var i = 0; i < sampleLength; i++)
            {
                sumAB += _sampleA.ElementAt(i) * _sampleB.ElementAt(i);
                sumX += _sampleA.ElementAt(i);
                sumY += _sampleB.ElementAt(i);
                sumXSquared += Math.Pow(_sampleA.ElementAt(i), 2);
                sumYSquared += Math.Pow(_sampleB.ElementAt(i), 2);
            }

            return (sumAB - (sumX * sumY) / sampleLength) /
                   (Math.Sqrt((sumXSquared - Math.Pow(sumX, 2) / sampleLength) *
                              (sumYSquared - Math.Pow(sumY, 2) / sampleLength)));
        }

        private double SpearmanCofficient()
        {
            var rankingA = ComputeRanking(_sampleA);
            var rankingB = ComputeRanking(_sampleB);
            var rankedSampleA = _sampleA.Select(x => rankingA[x]).ToArray();
            var rankedSampleB = _sampleB.Select(x => rankingB[x]).ToArray();
            var correctionFactor = CorrectionFactor(rankedSampleA) + CorrectionFactor(rankedSampleB);
            var length = rankedSampleA.Count(); 
            var sumDifferenceSquared = 0.0;

            for (int i = 0; i < rankedSampleA.Count(); i++)
                sumDifferenceSquared += Math.Pow(rankedSampleA[i] - rankedSampleB[i], 2);

            return 1 - ((6 * sumDifferenceSquared + correctionFactor) / (length * (length * length - 1)));
        }

        /* Computes the correction factor of ranked data. This is because
         * when there are ties in the data, the spearmans rank correlation
         * can not be equal to 1 or -1 without the correction factor. By
         * adding the correction for ties, it is possible to gain 1 or -1
         * results again.
         *
         * Formula:        (m * (m^2 - 1 )) / 12
         *
         */

        private static double CorrectionFactor(IEnumerable<double> rankedData)
        {
            return rankedData.GroupBy(y => y)
                .Where(k => k.Count() > 1)
                .Select(tie => tie.Count())
                .Select(m => (m * (m * m - 1)) / 12)
                .Sum();
        }


        /* Computes the ranks of items in a list by descending order.
         * When there are identical values in the list (a "tie") the
         * average of the ranks they would have occupied is used as
         * the rank for those items.
         */

        private static Dictionary<double, double> ComputeRanking(IEnumerable<double> values)
        {
            var ordered = values.OrderByDescending(x => x);
            var ranking = new Dictionary<double, double>();
            var duplicates = 1;
            var prevValue = 0.0;

            for (var i = 0; i < ordered.Count(); i++)
            {
                var value = ordered.ElementAt(i);

                if (ranking.ContainsKey(value))
                {
                    ranking[value] += i + 1;
                    duplicates++;
                }
                else
                {
                    if (i != 0)
                    {
                        ranking[prevValue] /= duplicates;
                    }
                    ranking.Add(value, i + 1);
                    duplicates = 1;
                }
                prevValue = value;
            }
            ranking[prevValue] /= duplicates;
            return ranking;
        }

//        private static double StandardDeviation(IEnumerable<double> sample)
//        {
//            var meanSample = Mean(sample);
//            var sampleSize = sample.Count();
//
//            return sample.Select(x => Math.Pow(x - meanSample, 2)).Sum() / sampleSize;
//        }

        private static double Mean(IEnumerable<double> sample) => sample.Sum() / sample.Count();
    }
}