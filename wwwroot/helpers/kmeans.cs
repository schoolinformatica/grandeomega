using System;
using System.Collections.Generic;
using System.Linq;

namespace kmeans
{
    public class Kmeans
    {
        public int Clusters { get; set; }
        public int Iterations { get; set; }
        public List<GenericVector> DataSet { get; set; }
        public Dictionary<int, GenericVector> Centroids { get; set; }
        private readonly Random _random = new Random();

        public void Run()
        {
            Centroids = GenerateRandomCentroids(Clusters);

            for (var i = 0; i < Iterations; i++)
            {
                var oldClusterValues = DataSet.Select(p => p.Cluster).ToList();
                RecalculateClusters();
                if (!IsChangedCluster(oldClusterValues, DataSet.Select(p => p.Cluster).ToList()))
                    break;
            }
        }

        private void RecalculateClusters()
        {
            DataSet.ForEach(vector => vector.Cluster = GetNearestCluster(vector));

            foreach (var key in Centroids.Keys.ToList())
            {
                var cluster = DataSet.Where(v => v.Cluster == key);
                if (cluster.Any())
                {
                    Centroids[key] = cluster
                        .Aggregate(new GenericVector(DataSet.First().Size), (x, y) => x.Sum(y))
                        .Devide(cluster.Count());
                }
            }
        }

        private static bool IsChangedCluster(IReadOnlyList<int> a, IReadOnlyList<int> b)
        {
            return a.Where((t, i) => t != b[i]).Any();
        }


        private int GetNearestCluster(GenericVector v)
        {
            var cluster = Centroids
                .OrderBy(Cluster => GenericVector.Distance(Cluster.Value, v))
                .Select(pair => pair.Key)
                .FirstOrDefault();
            return cluster;
        }

        private Dictionary<int, GenericVector> GenerateRandomCentroids(int k)
        {
            var clusters = new Dictionary<int, GenericVector>();
            var index = 0;
            k.Times(() => clusters.Add(index++, GetRandomVector()));

            return clusters;
        }

        private GenericVector GetRandomVector()
        {
            return DataSet.ElementAt(_random.Next(DataSet.Count));
        }

        public double SquaredErrors() {
            return DataSet
            .Select(x => Math.Pow(GenericVector.Distance(x, Centroids[x.Cluster]), 2))
            .Sum();

        }

        public void PrintClusters()
        {
            var clusters = DataSet.GroupBy(x => x.Cluster);
            foreach (var cluster in clusters)
            {
                Console.WriteLine("Cluster: " + cluster.ElementAt(0).Cluster);
                Console.WriteLine(cluster.Count());
            }
        }

    }
}