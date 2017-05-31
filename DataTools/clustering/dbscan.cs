using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using DataTools;

namespace Clustering
{
    public class Dbscan
    {
        private readonly float _radius;
        private readonly int _minPoints;
        private readonly List<ClusterPoint> _dataSet;

        public Dictionary<int, IEnumerable<GenericVector>> DataClusters { get; } =
            new Dictionary<int, IEnumerable<GenericVector>>();


        public Dbscan(float eps, int minPoints, IEnumerable<GenericVector> data)
        {
            _radius = eps;
            _minPoints = minPoints;
            _dataSet = data.Select(x => new ClusterPoint(x)).ToList();

            Run();
        }

        public void Run()
        {
            var cluster = 0;

            foreach (var clusterPoint in _dataSet)
            {
                if (clusterPoint.Visited) continue;

                clusterPoint.Visited = true;
                var neighbours = RegionQuery(clusterPoint.Vector);

                if (neighbours.Count < _minPoints)
                {
                    clusterPoint.Noise = true;
                }
                else
                {
                    cluster++;
                    ExpandCluster(clusterPoint, neighbours, cluster);
                }
            }
            
            foreach (var clust in _dataSet.Where(x => !x.Noise).GroupBy(x => x.Cluster))
            {
                var vectors = clust.Select(x => x.Vector);
                DataClusters.Add(clust.Key, vectors);
            }
        }


        private void ExpandCluster(ClusterPoint point, IEnumerable<ClusterPoint> neighbours, int cluster)
        {
            point.Cluster = cluster;
            var que = new Queue<ClusterPoint>(neighbours);


            while (que.Count > 0)
            {
                var neighbour = que.Dequeue();

                if (!neighbour.Visited)
                {
                    neighbour.Visited = true;
                    var neighboursOfNeighbour = RegionQuery(neighbour.Vector);
                    if (neighboursOfNeighbour.Count >= _minPoints)
                    {
                        foreach (var newNeighbour in neighboursOfNeighbour.Where(x => !x.Visited))
                            que.Enqueue(newNeighbour);
                    }
                }
                
                //TODO: Change this (Visitor pattern?)
                if (neighbour.Cluster < 0)
                    neighbour.Cluster = cluster;
            }
            
        }

        // return all points within Point's radius-neighborhood (including Point)

        private List<ClusterPoint> RegionQuery(GenericVector point)
        {
            var neighbours = new List<ClusterPoint>();

            foreach (var clusterPoint in _dataSet)
            {
                if (GenericVector.Distance(point, clusterPoint.Vector) <= _radius)
                    neighbours.Add(clusterPoint);
            }

            return neighbours;
        }
    }
}