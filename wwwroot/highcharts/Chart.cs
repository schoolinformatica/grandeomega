using System;
using System.Collections.Generic;
using Templatengine;

namespace Highcharts
{
    public class Chart : ITmplModel
    {
        private Dictionary<string, string> _replacers = new Dictionary<string, string>();
        private List<IChartsList> _data = new List<IChartsList>();

        public string Template { get; }

        public Chart()
        {
            Template = "scatterplot";
        }

        public void Set(string property, string value)
        {
            _replacers[property] = value;
        }

        public void AddDataSeries(DataSeries data)
        {
            if (_replacers.ContainsKey("data"))
                _replacers["data"] += data.ToTmplString();
            else
                _replacers["data"] = data.ToTmplString();

        }


        public string CreateTemplate()
        {
            return TmplEngine.CreateTemplate(this);
        }

        public List<Tuple<string, string>> GetReplacers()
        {
            var rep = new List<Tuple<string, string>>();

            foreach (var replacer in _replacers)
            {
                 rep.Add(new Tuple<string, string>(replacer.Key, replacer.Value));
            }

            return rep;
        }
    }
}