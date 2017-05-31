using System;
using System.Collections.Generic;
using Data;
using Regression;
using Xunit;

//Check if SOLID design principles
// k-nearest neighbors algorithm 
// neural networks

// https://en.wikipedia.org/wiki/Statistical_classification#Algorithms

namespace UnitTestProject
{
    public class UnitTest1
    {
        [Fact]
        public void TestPearson()
        {
            var sampleVectors = new List<GenericVector>();
            for (int i = 0; i < 100; i++)
            {
                sampleVectors.Add(new GenericVector(i, i + 5));
            }
            var regression = new SimpleRegression(sampleVectors);
            Assert.Equal(1, regression.PearsonCorrelation);
        }

        [Fact]
        public void TestSpearman()
        {
            var sampleVectors = new List<GenericVector>();
            for (int i = 0; i < 100; i++)
            {
                sampleVectors.Add(new GenericVector(i, i + 5));
            }
            var regression = new SimpleRegression(sampleVectors);
            Assert.Equal(1, regression.SpearmanCorrelation);
        }

        [Fact]
        public void TestGenericVector()
        {
            var a = new GenericVector(4, 4);
            var b = new GenericVector(6, 6);

            Assert.Equal(GenericVector.Distance(a, b), Math.Sqrt(8.0));

            var c = GenericVector.Sum(a, new GenericVector(6, 6));
            var d = GenericVector.Sum(b, new GenericVector(4, 4));

            Assert.Equal(GenericVector.Distance(c, d), 0);
        }
        
        [Fact]
        public void TestRegression()
        {
            var a = 4;
            var b = 2;
            var vectors = new List<GenericVector>();
            for (int i = 0; i < 10; i++)
            {
                vectors.Add(new GenericVector(i, a * i + b));
            }
            
            var regression = new SimpleRegression(vectors);
            var slope = regression.Slope;
            var intercept = regression.YIntercept;
            Assert.Equal(a, slope);
            Assert.Equal(b, intercept);
        }
    }
}
