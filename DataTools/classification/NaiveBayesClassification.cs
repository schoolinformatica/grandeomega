using System;
using System.Collections.Generic;
using System.Linq;
using DataTools.clustering;

namespace DataTools.classification
{
    public class NaiveBayesClassification : Classification
    {
        private readonly Dictionary<int, double> _clusterProbability;
        private readonly int _radius;

        public NaiveBayesClassification(Dictionary<int, IEnumerable<GenericVector>> trainingData, int radius) : base(trainingData)
        {
            _radius = radius;
            _clusterProbability = new Dictionary<int, double>();

            ComputeProbabilities();
        }

        public override int ClassifyPoint(GenericVector point)
        {
            return GetBiggestLikelihood(point);
        }

        private int GetBiggestLikelihood(GenericVector point)
        {
            var neighbours = GetNeighbours(point);
            var clusterLikelihood = GetClusterLikelihood(neighbours);
            var finalLikelihood = new Dictionary<int, double>();

            foreach (var likelihood in clusterLikelihood)
            {
                finalLikelihood[likelihood.Key] = likelihood.Value * _clusterProbability[likelihood.Key];
            }

            return finalLikelihood.OrderByDescending(x => x.Value).First().Key;
        }


        private List<ClusterPoint> GetNeighbours(GenericVector point)
        {
            var neighbours = new List<ClusterPoint>();

            foreach (var cluster in _trainingData)
            {
                foreach (var trainingPoint in cluster.Value)
                {
                    Console.WriteLine($"Distance {GenericVector.Distance(point, trainingPoint)}");
                    if (GenericVector.Distance(point, trainingPoint) < _radius)
                        neighbours.Add(new ClusterPoint(trainingPoint, cluster.Key));
                }
            }
            Console.WriteLine($"Neighbours: {neighbours.Count}");
            return neighbours;
        }

        private void ComputeProbabilities()
        {
            var totalPointsInData = _trainingData.Sum(x => x.Value.Count());
            Console.WriteLine($"training data size: {totalPointsInData}");

            foreach (var cluster in _trainingData)
            {
                _clusterProbability[cluster.Key] = (double) cluster.Value.Count() / totalPointsInData;
            }
        }

        private static Dictionary<int, double> GetClusterLikelihood(IReadOnlyCollection<ClusterPoint> neighbours)
        {
            var clusterLikelihood = new Dictionary<int, double>();
            var totalNeighbours = neighbours.Count();

            foreach (var neighbour in neighbours)
            {
                if (!clusterLikelihood.ContainsKey(neighbour.Cluster))
                    clusterLikelihood[neighbour.Cluster] = 0;

                clusterLikelihood[neighbour.Cluster] += (double) 1 / totalNeighbours;
            }

            return clusterLikelihood;
        }
    }
}