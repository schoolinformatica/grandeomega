using Data;

namespace DataTools
{
    public class ClusterPoint
    {
        public bool Visited { get; set; }
        public bool Noise { get; set; }
        public GenericVector Vector { get; }

        public int Cluster { get; set; }

        public ClusterPoint(GenericVector vector, int cluster = -1)
        {
            Vector = vector;
            Cluster = cluster;
        } 
    }
}