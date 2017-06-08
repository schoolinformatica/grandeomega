using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataTools.clustering;
using DataTools.utils;

namespace DataTools.classification
{
    //To fasten up the process, use a KDtree
    
    public class KnearestClassification
    {
        private readonly Dictionary<int, IEnumerable<GenericVector>> _trainingData;
        private readonly int _k;

        public KnearestClassification(Dictionary<int, IEnumerable<GenericVector>> traingingData, int k)
        {
            _trainingData = traingingData;
            _k = k;
        }

        public int ClassifyPoint(GenericVector point)
        {
            var nearestPoints = new PriorityQue<ClusterPoint>();

            foreach (var cluster in _trainingData)
            {
                foreach (var clusterPoint in cluster.Value)
                {
                    var distanceToClusterPoint = GenericVector.Distance(point, clusterPoint);
                    
                    if (nearestPoints.Count < _k)
                    {
                        nearestPoints.Insert(distanceToClusterPoint, new ClusterPoint(clusterPoint, cluster.Key));
                    }
                    else if (distanceToClusterPoint < nearestPoints.Peek().Priority)
                    {
                        nearestPoints.Pop();
                        nearestPoints.Insert(distanceToClusterPoint, new ClusterPoint(clusterPoint, cluster.Key));
                    }
                }

            }
            
            return GetBiggestCluster(nearestPoints);
        }


        private static int GetBiggestCluster(PriorityQue<ClusterPoint> nearestPoints)
        {
            var clusters = new Dictionary<int, int>();
            
            while (!nearestPoints.IsEmpty)
            {
                var queItem = nearestPoints.Pop();
                
                if (!clusters.ContainsKey(queItem.Item.Cluster))
                {
                    clusters[queItem.Item.Cluster] = 0;
                }
                
                clusters[queItem.Item.Cluster] += 1;
            }

            return clusters.OrderBy(x => x.Value).First().Key;
        }
        
    }
}