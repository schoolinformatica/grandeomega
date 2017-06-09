using DataTools.clustering;
using DataTools.correlation;
using DataTools.regression;
using Highcharts;

namespace webapp.wwwroot.scripts
{
    public class HighchartsAdapter
    {
        private readonly Chart _chart;

        public HighchartsAdapter(Highchart chart)
        {
            _chart = new Chart(chart);
        }

        public string CreateTemplate()
        {
            return _chart.CreateTemplate();
        }

        public void SetDivId(string divId)
        {
            _chart.SetDivId(divId);
        }

        public void SetTitle(string title)
        {
            _chart.SetTitle(title);
        }

        public void SetXlabel(string label)
        {
            _chart.SetXlabel(label);
        }

        public void SetYlabel(string label)
        {
            _chart.SetYlabel(label);
        }
        
        public void AddClusters(Clustering clustering)
        {
            foreach (var cluster in clustering.DataClusters)
            {
                var dataset = new Dataset(cluster.Value);
                var name = $"Cluster {cluster.Key}";
                var series = new DataSeries(Highchart.Scatterplot, dataset, name);
                
                _chart.AddDataSeries(series);
            }
        }

        public void AddRegression(Regression regression)
        {
            var set = new Dataset(regression.GetRegressionLine());
            var series = new DataSeries(Highchart.Regression, set, "Regression line");
            
            series.SetMarker(false);
            _chart.AddDataSeries(series);
        }

        public void AddCorrelation(Correlation correlation)
        {
            var corr = correlation.GetCorrelationCoefficient();
            
            _chart.SetSubtitle($"Correlation: {corr}");
        }
    }
}