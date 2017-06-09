using System.Collections.Generic;

namespace DataTools.correlation
{
    
    public abstract class Correlation
    {
        protected readonly IEnumerable<Vector2> Data;

        protected Correlation(IEnumerable<Vector2> data)
        {
            Data = data;
        }

        public abstract double GetCorrelationCoefficient();
              
    }
}