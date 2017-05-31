using Data;

namespace DataTools
{
    public class ClusterPoint
    {
        public bool Visited { get; set; }
        public bool Noise { get; set; }
        public GenericVector Vector { get; }

        public int Cluster { get; set; } = -1;

        public ClusterPoint(GenericVector vector) => Vector = vector;
    }
}