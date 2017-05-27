using System;
using System.Collections.Generic;
using Templatengine;

namespace Highcharts
{
    public class DataSeries : ITmplModel
    {
        public string Template { get; }
        private Dictionary<string, string> _replacers = new Dictionary<string, string>();

        public DataSeries(Enum type, IChartsList data, string name, bool marker = true, bool tracking = false)
        {
            _replacers["type"] = GetChartType(type);
            _replacers["data"] = data.ToChartsList();
            _replacers["name"] = name;
            _replacers["marker"] = marker.ToString().ToLower();
            _replacers["mousetracking"] = tracking.ToString().ToLower();
            Template = "dataseries";
        }

        public void SetType(string type) => _replacers["type"] = type;
        public void SetName(string name) => _replacers["name"] = name;
        public void SetMarker(bool marker) => _replacers["marker"] = marker.ToString().ToLower();
        public void SetMouseTracking(bool tracking) => _replacers["mousetracking"] = tracking.ToString().ToLower();
        public void SetData(IChartsList data) => _replacers["data"] = data.ToChartsList();

        public string ToTmplString()
        {
            return CreateTemplate();
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

        private static string GetChartType(Enum type)
        {
            switch (type)
            {
                    case Highcarts.Scatterplot:
                        return "scatter";

                    case Highcarts.Regression:
                        return "line";

                    default:
                        throw  new Exception("Invalid chart type");
            }
        }

    }
}