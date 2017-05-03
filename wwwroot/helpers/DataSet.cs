using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Data
{
    public class DataSet : IEnumerable<GenericVector>
    {
        #region Fields

        private List<GenericVector> _data;

        #endregion


        #region Properties

        public int Dimensions => _data.First().Size;

        #endregion


        #region Constructors

        public DataSet(List<GenericVector> vectors)
        {
            _data = vectors;
        }

        #endregion


        #region Methods

        public List<GenericVector> ToList()
        {
            return _data;
        }

        public override string ToString()
        {
            return _data.Aggregate("[", (current, vector) => current + (vector.ToString() + ",")) + "]";
        }

        #endregion


        #region IEnumberable Methods

        // IEnumberable is implemented to make the class able
        // to be used in Linq expressions.

        public IEnumerator<GenericVector> GetEnumerator()
        {
            return (_data as IEnumerable<GenericVector>).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}