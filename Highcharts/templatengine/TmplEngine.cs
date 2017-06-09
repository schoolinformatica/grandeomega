using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Highcharts.templatengine;

namespace Highcharts
{
    public static class TmplEngine
    {
        private const string LeftToken = "<%";
        private const string RightToken = "%>";

        public static string CreateTemplate(ITmplModel model)
        {
            string bodyFile;
            
            var template = model.Template.Value;
            var replacers = model.GetReplacers();
            var r = Resources.LoadFile(template);
            using (var reader = new StreamReader(r))
            {
                bodyFile = reader.ReadToEnd();

                foreach (var replacer in replacers)
                {
                    bodyFile = bodyFile.Replace($"{LeftToken}{replacer.Item1}{RightToken}", replacer.Item2);
                }
            }

            return bodyFile;
        }
    }
}