using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Highcharts;

namespace Data
{
    public class DataSet : IEnumerable<GenericVector>, IChartsList
    {
        private List<GenericVector> _data;

        public int Dimensions => _data.First().Size;


        public DataSet(List<GenericVector> vectors) => _data = vectors;


        public List<GenericVector> ToList() => _data;

        public string ToChartsList() => ToString();

        public override string ToString() =>
            _data.Aggregate("[", (current, vector) => current + (vector.ToString() + ",")) + "]";


        // IEnumberable is implemented to make the class able
        // to be used in Linq expressions.

        public IEnumerator<GenericVector> GetEnumerator() => (_data as IEnumerable<GenericVector>).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}