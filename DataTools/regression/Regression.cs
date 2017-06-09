using System.Collections.Generic;

namespace DataTools.regression
{
    public abstract class Regression
    {
        public abstract IEnumerable<GenericVector> GetRegressionLine();
    }
}