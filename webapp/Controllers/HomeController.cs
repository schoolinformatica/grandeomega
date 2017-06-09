using System;
using System.Collections.Generic;
using System.Linq;
using DataTools;
using DataTools.classification;
using DataTools.clustering;
using DataTools.correlation;
using DataTools.regression;
using Highcharts;
using Microsoft.AspNetCore.Mvc;
using webapp.wwwroot.models;
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
        public string CreateGraph(Stud dataA, Stud dataB, bool kmeans, bool dbscan, bool linearregression,
            bool polynomialregression, bool pearsoncorrelation, bool spearmancorrelation, bool classifyungraded)
        {
            var gradedStudents = Students.StudentsGraded;
            
            if (classifyungraded)
            {
                Console.WriteLine($"students graded {gradedStudents.Count}");
                var grades = new Dictionary<int, double>();
                var clusters = new Dbscan(210, 3,
                    Students.StudentsGraded.Select(x => x.ToGenericVector(Stud.Attempts, Stud.Class, Stud.FailRatio,
                        Stud.Fails, Stud.Succeeds, Stud.SuccessRatio, Stud.Grade)));

                foreach (var cluster in clusters.DataClusters)
                {
                    grades[cluster.Key] = cluster.Value.Sum(x => x[6]) / cluster.Value.Count();
                    Console.WriteLine($"Average grade: {grades[cluster.Key]}");
                }

                var classification = new NaiveBayesClassification(clusters.DataClusters, 50000);
                foreach (var student in Students.StudentsUngraded)
                {
                    var cluster = classification.ClassifyPoint(student.ToGenericVector(Stud.Attempts, Stud.Class,
                        Stud.FailRatio, Stud.Fails, Stud.Succeeds, Stud.SuccessRatio, Stud.Grade));

                    student.Grade = (int)grades[cluster];
                    gradedStudents.Add(student);
                }
            }
            
            foreach (var student in gradedStudents)
            {
                student.Filter();
            }

            var data = new Dataset(gradedStudents.Select(x => x.ToGenericVector(dataA, dataB)));

            var highChart = new HighchartsAdapter(Highchart.Scatterplot);

            

            //Dbscan removes outliers, so we have to change are dataset afterwards
            if (dbscan)
            {
                var newData = new List<GenericVector>();
                var dBscan = new Dbscan(50, 3, data);
                foreach (var cluster in dBscan.DataClusters)
                {
                    newData.AddRange(cluster.Value);
                }
                data = new Dataset(newData);
                highChart.AddClusters(dBscan);
            }

            if (kmeans)
                highChart.AddClusters(new Kmeans(4, 100, data));


            if (linearregression)
                highChart.AddRegression(new LinearRegression(data.Select(x => x.ToVector2())));

            if (polynomialregression)
                highChart.AddRegression(new PolynomialRegression(data.Select(x => x.ToVector2()), 3));

            if (pearsoncorrelation)
                highChart.AddCorrelation(new PearsonCorrelation(data.Select(x => x.ToVector2())));

            if (spearmancorrelation)
                highChart.AddCorrelation(new SpearmanCorrelation(data.Select(x => x.ToVector2())));


            highChart.SetDivId("plotkmeans");
            highChart.SetTitle($"{dataA} vs {dataB}");
            highChart.SetXlabel(dataA.ToString());
            highChart.SetYlabel(dataB.ToString());

            return highChart.CreateTemplate();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}