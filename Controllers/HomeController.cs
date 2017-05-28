using System;
using System.Collections.Generic;
using System.Linq;
using Clustering;
using Data;
using Highcharts;
using models;
using Microsoft.AspNetCore.Mvc;
using Regression;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string CreateGraph(Stud DataA, Stud DataB, bool kmeans, bool dbscan, bool simpleregression)
        {
            var dataSeries = new List<DataSeries>();
            var title = $"Plot of {DataA} vs {DataB}";

            Console.WriteLine(
                $" \nDataA {DataA}, DataB {DataB}, Kmeans {kmeans}, Dbscan {dbscan}, Simpleregression {simpleregression}\n");


            var gradedStudents = Students.students.Where(x => x.Grade > 0);

            foreach (var student in gradedStudents)
            {
                student.Filter();
            }

            var data = new DataSet(gradedStudents.Select(x => x.ToGenericVector(DataA, DataB)).ToList());

            if (kmeans)
            {
                var km = new Kmeans(4, 100, data);
                var clusterId = 1;

                foreach (var cluster in km.DataClusters)
                {
                    var header = $"Cluster {clusterId++}";
                    dataSeries.Add(new DataSeries(Highcarts.Scatterplot, cluster, header));
                }
            }

            if (dbscan)
            {
                var db = new Dbscan(50, 3, data);
                var clusterId = 1;

                foreach (var cluster in db.DataClusters)
                {
                    var header = $"Cluster {clusterId++}";
                    dataSeries.Add(new DataSeries(Highcarts.Scatterplot, cluster, header));
                }
            }

            if (simpleregression)
            {
                var regression = new SimpleRegression(data);
                var dataSer = new DataSeries(Highcarts.Regression, regression.GetLinearRegression(), "Regression Line");
                dataSeries.Add(dataSer);
                Console.WriteLine($"Pearson: {regression.PearsonCorrelation} Spearman: {regression.SpearmanCorrelation}");
            }

            var plotDbscan = new Chart("plotdbscan");

            foreach (var dataSerie in dataSeries)
            {
                plotDbscan.AddDataSeries(dataSerie);
            }

            plotDbscan.Set("chart", "scatter");
            plotDbscan.Set("title", title);

            return plotDbscan.CreateTemplate();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}