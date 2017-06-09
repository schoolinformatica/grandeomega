using System;
using System.Collections.Generic;
using Highcharts.templatengine;

namespace Highcharts
{
    public class DataSeries : ITmplModel
    {
        public TemplateFile Template { get; } = TemplateFile.Dataseries;

        private readonly Dictionary<Replacer, string> _replacers = new Dictionary<Replacer, string>();

        public DataSeries(Highchart type, IChartsList data, string name, bool marker = true, bool tracking = false)
        {
            _replacers[Replacer.Type] = type.Value;
            _replacers[Replacer.Data] = data.ToChartsList();
            _replacers[Replacer.Name] = name;
            _replacers[Replacer.Marker] = marker.ToString().ToLower();
            _replacers[Replacer.Mousetracking] = tracking.ToString().ToLower();
        }

        public void SetType(string type) => _replacers[Replacer.Type] = type;
        public void SetName(string name) => _replacers[Replacer.Name] = name;
        public void SetMarker(bool marker) => _replacers[Replacer.Marker] = marker.ToString().ToLower();
        public void SetMouseTracking(bool tracking) => _replacers[Replacer.Mousetracking] = tracking.ToString().ToLower();
        public void SetData(IChartsList data) => _replacers[Replacer.Data] = data.ToChartsList();

        public string ToTmplString() => CreateTemplate();

        public string CreateTemplate() => TmplEngine.CreateTemplate(this);

        public List<Tuple<string, string>> GetReplacers()
        {
            var rep = new List<Tuple<string, string>>();

            foreach (var replacer in _replacers)
            {
                rep.Add(new Tuple<string, string>(replacer.Key.Value, replacer.Value));
            }

            return rep;
        }

       
    }
}