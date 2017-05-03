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

        public List<KGenericVector> DataSet { get; set; }

        private int Clusters { get; }
        private int Iterations { get; }
        private Dictionary<int, KGenericVector> Centroids { get; set; }


        //CONSTRUCTORS
        public Kmeans(int k, int iterations, DataSet dataSet)
        {
            Clusters = k;
            Iterations = iterations;
            DataSet = dataSet.Select(x => new KGenericVector(x)).ToList();
        }


        //METHODS
        public void Run()
        {
            Centroids = GenerateRandomCentroids(Clusters);

            for (var i = 0; i < Iterations; i++)
            {
                var oldClusterValues = DataSet.Select(point => point.Cluster).ToList();
                RecalculateClusters();
                if (!IsChangedCluster(oldClusterValues, DataSet.Select(p => p.Cluster).ToList()))
                    break;
            }
        }

        public double GetSquaredErrors()
        {
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


        private void RecalculateClusters()
        {
            DataSet.ForEach(vector => vector.Cluster = GetNearestCluster(vector));

            foreach (var key in Centroids.Keys.ToList())
            {
                var cluster = DataSet.Where(v => v.Cluster == key);
                if (cluster.Any())
                {
                    Centroids[key] = cluster
                        .Aggregate(new KGenericVector(DataSet.First().Size), (x, y) => x.Sum(y))
                        .Devide(cluster.Count());
                }
            }
        }

        private int GetNearestCluster(GenericVector v)
        {
            return Centroids
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
            return DataSet.ElementAt(_random.Next(DataSet.Count));
        }


        private static bool IsChangedCluster(IEnumerable<int> a, IReadOnlyList<int> b)
        {
            return a.Where((t, i) => t != b[i]).Any();
        }
    }
}