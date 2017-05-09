using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Helpers;

namespace Clustering
{
    public class Kmeans
    {
        //FIELDS
        private readonly Random _random = new Random();


        //PROPERTIES

        private readonly int _clusters;
        private readonly int _iterations;
        private readonly List<KGenericVector> _dataSet;
        private Dictionary<int, KGenericVector> _centroids;


        //CONSTRUCTORS
        public Kmeans(int k, int iterations, DataSet dataSet)
        {
            _clusters = k;
            _iterations = iterations;
            _dataSet = dataSet.Select(x => new KGenericVector(x)).ToList();
        }


        //METHODS
        public void Run()
        {
            _centroids = GenerateRandomCentroids(_clusters);

            for (var i = 0; i < _iterations; i++)
            {
                var oldClusterValues = _dataSet.Select(point => point.Cluster).ToList();
                RecalculateClusters();
                if (!IsChangedCluster(oldClusterValues, _dataSet.Select(p => p.Cluster).ToList()))
                    break;
            }
        }

        public double GetSquaredErrors()
        {
            return _dataSet
                .Select(x => Math.Pow(GenericVector.Distance(x, _centroids[(int) x.Cluster]), 2))
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
            _dataSet.ForEach(vector => vector.Cluster = GetNearestCluster(vector));

            foreach (var key in _centroids.Keys.ToList())
            {
                var cluster = _dataSet.Where(v => v.Cluster == key);
                if (cluster.Any())
                {
                    _centroids[key] = cluster
                        .Aggregate(new KGenericVector(_dataSet.First().Size), (x, y) => x.Sum(y))
                        .Devide(cluster.Count());
                }
            }
        }

        private int GetNearestCluster(GenericVector v)
        {
            return _centroids
                .OrderBy(centroid => GenericVector.Distance(centroid.Value, v))
                .Select(centroid => centroid.Key)
                .First();
        }

        private Dictionary<int, KGenericVector> GenerateRandomCentroids(int kAmount)
        {
            var clusters = new Dictionary<int, KGenericVector>();
            var index = 0;
            kAmount.Times(() => clusters.Add(index++, GetRandomVector()));

            return clusters;
        }

        private KGenericVector GetRandomVector()
        {
            return _dataSet.ElementAt(_random.Next(_dataSet.Count));
        }


        private static bool IsChangedCluster(List<int?> a, List<int?> b)
        {
            return a.Where((t, i) => t != b[i]).Any();
        }
    }
}