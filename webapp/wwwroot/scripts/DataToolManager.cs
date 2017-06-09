using System;
using System.Collections.Generic;
using DataTools;
using DataTools.clustering;
using DataTools.correlation;
using DataTools.regression;

namespace webapp.wwwroot.scripts
{
    public class DataToolManager
    {
        private Dataset _dataset;
        private Clustering _clustering;
        private Correlation _correlation;
        private Regression _regression;
        

        public DataToolManager(Dataset dataset)
        {
            _dataset = dataset;
        }

        public void SetClustering(Clustering clustering) => _clustering = clustering;
        public void SetCorrelation(Correlation correlation) => _correlation = correlation;
        public void SetRegression(Regression regression) => _regression = regression;

        public Dictionary<int, IEnumerable<GenericVector>> GetClusters()
        {
            return _clustering.DataClusters;
        }

        public IEnumerable<GenericVector> GetRegressionLine()
        {
            return _regression.GetRegressionLine();
        }

        public double GetCorrelation()
        {
            return Math.Round(_correlation.GetCorrelationCoefficient(), 3);
        }
    }
}