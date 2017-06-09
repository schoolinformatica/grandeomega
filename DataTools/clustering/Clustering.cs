using System.Collections.Generic;

namespace DataTools.clustering
{
    public abstract class Clustering
    {
        public Dictionary<int, IEnumerable<GenericVector>> DataClusters { get; protected set; }
    }
}