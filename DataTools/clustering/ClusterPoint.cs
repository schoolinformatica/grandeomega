namespace DataTools.clustering
{
    
    internal class ClusterPoint
    {
        internal bool Visited { get; set; }
        internal bool Noise { get; set; }
        internal GenericVector Vector { get; }

        internal int Cluster { get; set; }
          
        internal ClusterPoint(GenericVector vector, int cluster = -1)
        {
            Vector = vector;
            Cluster = cluster;
        } 
    }
}