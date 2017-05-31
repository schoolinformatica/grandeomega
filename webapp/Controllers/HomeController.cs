using System;
using System.Collections.Generic;
using System.Linq;
using Clustering;
using Data;
using Highcharts;
using models;
using Microsoft.AspNetCore.Mvc;
using Regression;
using webapp.wwwroot.scripts;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string CreateGraph(Stud dataA, Stud dataB, bool kmeans, bool dbscan, bool simpleregression)
        {
            var dataSeries = new List<DataSeries>();
            var title = $"Plot of {dataA} vs {dataB}";
            var chart = new Chart(Highchart.Scatterplot);

            Console.WriteLine(
                $" \nDataA {dataA}, DataB {dataB}, Kmeans {kmeans}, Dbscan {dbscan}, Simpleregression {simpleregression}\n");


            var gradedStudents = Students.students.Where(x => x.Grade > 0);

            foreach (var student in gradedStudents)
            {
                student.Filter();
            }

            var data = new Dataset(gradedStudents.Select(x => x.ToGenericVector(dataA, dataB)));

            if (kmeans)
            {
                var clustering = new Kmeans(4, 100, data);

                foreach (var cluster in clustering.DataClusters)
                {
                    var header = $"Cluster {cluster.Key}";
                    dataSeries.Add(new DataSeries(Highchart.Scatterplot, new Dataset(cluster.Value), header));
                }
            }

            if (dbscan)
            {
                var newDataset = new List<GenericVector>();
                var db = new Dbscan(50, 3, data);

                foreach (var cluster in db.DataClusters)
                {
                    var header = $"Cluster {cluster.Key}";
                    dataSeries.Add(new DataSeries(Highchart.Scatterplot, new Dataset(cluster.Value), header));
                    newDataset.AddRange(cluster.Value);
                }
                
                data = new Dataset(newDataset);
            }

            if (simpleregression)
            {
                var regression = new SimpleRegression(data);
                var dataSer = new DataSeries(Highchart.Regression, new Dataset(regression.GetLinearRegression()),
                    "Regression Line");
                
                dataSeries.Add(dataSer);
                chart.SetSubtitle(regression.PearsonCorrelation.ToString());
          
            }

            

            foreach (var dataSerie in dataSeries)
                chart.AddDataSeries(dataSerie);
            

            chart.SetDivId("plotdbscan");
            chart.SetTitle(title);
            chart.SetXlabel(dataA.ToString());
            chart.SetYlabel(dataB.ToString());

            return chart.CreateTemplate();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}