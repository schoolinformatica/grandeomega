using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Data;
using Highcharts;

namespace webapp.wwwroot.scripts
{
    public class Dataset : IEnumerable<GenericVector>, IChartsList
    {
        private readonly IEnumerable<GenericVector> _vectors; 
        
        public Dataset(IEnumerable<GenericVector> vectors) => _vectors = vectors;


        public string ToChartsList() => ToString();
        
        public override string ToString() =>
            _vectors.Aggregate("[", (current, vector) => current + (vector.ToString() + ",")) + "]";
        
        public IEnumerator<GenericVector> GetEnumerator() => _vectors.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}