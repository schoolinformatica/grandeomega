using System;
using System.Collections.Generic;
using System.Linq;
using Data;

namespace Clustering
{
    public class Dbscan
    {

        public List<DataSet> DataClusters => dataClusters;

        private readonly float _radius;
        private readonly int _minPoints;
        private readonly List<KGenericVector> _dataSet;
        private List<DataSet> dataClusters  = new List<DataSet>();

        public Dbscan(float eps, int minPoints, DataSet data, bool run = true)
        {
            _radius = eps;
            _minPoints = minPoints;
            _dataSet = data.Select(x => new KGenericVector(x)).ToList();

            if(run)
                Run();
        }

        public void Run()
        {
            var cluster = 0;

            for (var i = 0; i < _dataSet.Count; i++)
            {
                var point = _dataSet[i];

                if (point.Visited) continue;

                point.Visited = true;
                var neighbours = RegionQuery(point);
                if (neighbours.Count < _minPoints)
                {
                    point.Noise = true;
                }
                else
                {
                    cluster++;
                    ExpandCluster(point, neighbours, cluster);
                }
            }

            foreach (var clust in _dataSet.Where(x => !x.Noise).GroupBy(x => x.Cluster))
            {
                var vectors = clust.Select(x => x as GenericVector).ToList();
                dataClusters.Add(new DataSet(vectors));
            }
        }


        private void ExpandCluster(KGenericVector point, List<KGenericVector> neighbours, int cluster)
        {
            point.Cluster = cluster;

            for (var i = 0; i < neighbours.Count; i++)
            {
                var neighbour = neighbours[i];

                if (!neighbour.Visited)
                {
                    neighbour.Visited = true;
                    var neighboursOfNeighbour = RegionQuery(neighbour);
                    if (neighboursOfNeighbour.Count >= _minPoints)
                        neighbours.AddRange(neighboursOfNeighbour);
                }

                if (neighbour.Cluster == null)
                {
                    neighbour.Cluster = cluster;
                }
            }
        }

        // return all points within Point's radius-neighborhood (including Point)

        private List<KGenericVector> RegionQuery(GenericVector point)
        {
            var neighbours = new List<KGenericVector>();

            for (var i = 0; i < _dataSet.Count; i++)
            {
                var currPoint = _dataSet[i];

                if (GenericVector.Distance(point, currPoint) <= _radius)
                    neighbours.Add(currPoint);
            }

            return neighbours;
        }
    }
}