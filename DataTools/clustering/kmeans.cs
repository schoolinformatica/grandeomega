using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using DataTools;
using DataTools.utils;

namespace Clustering
{
    public class Kmeans
    {
        private readonly Random _random = new Random();
        private readonly int _clusters;
        private readonly int _iterations;
        private readonly List<ClusterPoint> _dataSet;
        private Dictionary<int, GenericVector> _centroids;

        public Dictionary<int, IEnumerable<GenericVector>> DataClusters { get; } =
            new Dictionary<int, IEnumerable<GenericVector>>();


        public Kmeans(int k, int iterations, IEnumerable<GenericVector> dataSet)
        {
            _clusters = k;
            _iterations = iterations;
            _dataSet = dataSet.Select(x => new ClusterPoint(x)).ToList();

            Run();
        }


        public void Run()
        {
            _centroids = GenerateRandomCentroids(_clusters);

            for (var i = 0; i < _iterations; i++)
            {
                var oldClusterValues = _dataSet.Select(point => point.Cluster);
                RecalculateClusters();
                RecalculateCentroids();
                if (!IsChangedCluster(oldClusterValues, _dataSet.Select(p => p.Cluster).ToList()))
                    break;
            }

            foreach (var cluster in _dataSet.GroupBy(x => x.Cluster))
            {
                var vectors = cluster.Select(x => x.Vector);
                DataClusters[cluster.Key] = vectors;
            }
        }

        public double GetSquaredErrors()
        {
            return _dataSet
                .Select(x => Math.Pow(GenericVector.Distance(x.Vector, _centroids[(int) x.Cluster]), 2))
                .Sum();
        }

        public void PrintClusters()
        {
            var clusters = _dataSet.GroupBy(x => x.Cluster);
            foreach (var cluster in clusters)
            {
                Console.WriteLine("Cluster: " + cluster.ElementAt(0).Cluster);
                Console.WriteLine(cluster.Count());
            }
        }


        private void RecalculateClusters()
        {
            foreach (var clusterPoint in _dataSet)
            {
                clusterPoint.Cluster = GetNearestCluster(clusterPoint.Vector);
            }
        }

        private void RecalculateCentroids()
        {
            var newCentroids = new Dictionary<int, GenericVector>();

            foreach (var centroid in newCentroids)
            {
                var cluster = _dataSet.Where(x => x.Cluster == centroid.Key);
                var sum = new GenericVector(cluster.First().Vector.Size);

                foreach (var clusterPoint in cluster)
                {
                    sum = GenericVector.Sum(sum, clusterPoint.Vector);
                }

                newCentroids[centroid.Key] = GenericVector.Devide(sum, cluster.Count());
            }
        }

        private int GetNearestCluster(GenericVector v)
        {
            return _centroids
                .OrderBy(centroid => GenericVector.Distance(centroid.Value, v))
                .Select(centroid => centroid.Key)
                .First();
        }

        private Dictionary<int, GenericVector> GenerateRandomCentroids(int kAmount)
        {
            var clusters = new Dictionary<int, GenericVector>();
            var index = 1;
            kAmount.Times(() => clusters.Add(index++, GetRandomVector()));

            return clusters;
        }

        private GenericVector GetRandomVector() => _dataSet.ElementAt(_random.Next(_dataSet.Count())).Vector;


        private static bool IsChangedCluster(IEnumerable<int> a, IReadOnlyList<int> b)
        {
            return a.Where((t, i) => t != b[i]).Any();
        }
    }
}