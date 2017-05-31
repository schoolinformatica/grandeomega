using System;
using System.Collections.Generic;

namespace Highcharts
{
    public interface ITmplModel
    {
        string Template { get; }

        string CreateTemplate();
        List<Tuple<string, string>> GetReplacers();

    }
}