using System;
using System.Collections.Generic;
using System.Linq;

namespace DataTools.correlation
{
    public class SpearmanCorrelation : Correlation
    {
        public SpearmanCorrelation(IEnumerable<Vector2> data) : base(data)
        {
        }

        public override double GetCorrelationCoefficient()
        {
            return SpearmanCofficient();
        }
        
        private double SpearmanCofficient()
        {
            var rankingX = ComputeRanking(Data.Select(x => x.X));
            var rankingY = ComputeRanking(Data.Select(x => x.Y));
            var rankedSampleX = Data.Select(x => rankingX[x.X]).ToArray();
            var rankedSampleY = Data.Select(x => rankingY[x.Y]).ToArray();
            var correctionFactor = CorrectionFactor(rankedSampleX) + CorrectionFactor(rankedSampleY);
            var length = rankedSampleX.Count(); 
            var sumDifferenceSquared = 0.0;

            for (int i = 0; i < rankedSampleX.Count(); i++)
                sumDifferenceSquared += Math.Pow(rankedSampleX[i] - rankedSampleY[i], 2);

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
    }
}