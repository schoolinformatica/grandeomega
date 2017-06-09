using System;
using System.Collections.Generic;
using Highcharts.templatengine;

namespace Highcharts
{
    public class Chart : ITmplModel
    {
        private readonly Dictionary<Replacer, string> _replacers = new Dictionary<Replacer, string>();

        public TemplateFile Template { get; } = TemplateFile.Chart;

        public Chart(Highchart chartType) => _replacers[Replacer.Chart] = chartType.Value;


        public void SetDivId(string divid) => _replacers[Replacer.DivId] = divid;
        public void SetChartType(string type) => _replacers[Replacer.Chart] = type;
        public void SetTitle(string title) => _replacers[Replacer.Title] = title;
        public void SetSubtitle(string subtitle) => _replacers[Replacer.Subtitle] = subtitle;
        public void SetXlabel(string xlabel) => _replacers[Replacer.Xlabel] = xlabel;
        public void SetYlabel(string ylabel) => _replacers[Replacer.Ylabel] = ylabel;
        public void SetXtooltip(string xtooltip) => _replacers[Replacer.Xtooltip] = xtooltip;
        public void SetYtooltip(string ytooltip) => _replacers[Replacer.Ytooltip] = ytooltip;

        public string CreateTemplate() => TmplEngine.CreateTemplate(this);


        public void AddDataSeries(DataSeries data)
        {
            if (_replacers.ContainsKey(Replacer.Data))
                _replacers[Replacer.Data] += data.ToTmplString();
            else
                _replacers[Replacer.Data] = data.ToTmplString();
        }


        public List<Tuple<string, string>> GetReplacers()
        {
            var replacers = new List<Tuple<string, string>>();

            foreach (var replacer in _replacers)
            {
                replacers.Add(new Tuple<string, string>(replacer.Key.Value, replacer.Value));
            }

            return replacers;
        }
    }
}