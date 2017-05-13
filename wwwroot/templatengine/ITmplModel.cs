using System;
using System.Collections.Generic;

namespace Templatengine
{
    public interface ITmplModel
    {
        string Template { get; }

        string CreateTemplate();
        List<Tuple<string, string>> GetReplacers();

    }
}