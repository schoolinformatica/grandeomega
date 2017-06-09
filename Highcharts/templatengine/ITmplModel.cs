using System;
using System.Collections.Generic;

namespace Highcharts.templatengine
{
    public interface ITmplModel
    {
        TemplateFile Template { get; }

        string CreateTemplate();
        List<Tuple<string, string>> GetReplacers();

    }
}