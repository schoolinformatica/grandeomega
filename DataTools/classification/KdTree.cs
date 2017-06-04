using System.Collections.Generic;
using System.Linq;
using Data;
using Tree;

namespace DataTools.classification
{
    public class KDTree : KdTree<GenericVector>
    {
        public KDTree(ITree<GenericVector> left, GenericVector value, ITree<GenericVector> right) : base(left, value,
            right)
        {
        }

        public override void Insert(GenericVector value)
        {
            throw new System.NotImplementedException();
        }


        public GenericVector[] GetNearestNeighbours(GenericVector point, int neighbours)
        {
            
        }

        public static ITree<GenericVector> OfList(IEnumerable<GenericVector> list, int dimensions)
        {
            const int firstDimension = 0;

            return CreateTreeOfList(list.ToList(), firstDimension, dimensions);
        }

        private static ITree<GenericVector> CreateTreeOfList(List<GenericVector> list, int dimension, int dimensionCount)
        {
            var listCount = list.Count;
            var mean = listCount / 2;
            var currDimension = dimension % dimensionCount;
            list = list.OrderBy(vector => vector[currDimension]).ToList();

            if (listCount == 0)
            {
                return new Empty<GenericVector>();
            }
            

            return new KDTree(CreateTreeOfList(list.GetRange(0, mean), currDimension + 1, dimensionCount), list[mean],
                CreateTreeOfList(list.GetRange(mean + 1, listCount - mean - 1), currDimension + 1, dimensionCount));
        }
    }
}