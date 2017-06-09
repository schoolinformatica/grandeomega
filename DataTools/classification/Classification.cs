using System.Collections.Generic;

namespace DataTools.classification
{
    public abstract class Classification
    {
        protected readonly Dictionary<int, IEnumerable<GenericVector>> _trainingData;
        
        protected Classification(Dictionary<int, IEnumerable<GenericVector>> trainingData)
        {
            _trainingData = trainingData;
        }

        public abstract int ClassifyPoint(GenericVector point);

    }
}