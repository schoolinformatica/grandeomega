using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Highcharts
{
    public static class TmplEngine
    {
        private const string Prefix = "Highcharts.Files.";
        private const string FileExtension = ".tmpl";
        private const string LeftToken = "<%";
        private const string RightToken = "%>";

        public static string CreateTemplate(ITmplModel model)
        {
            string bodyFile;
            
            var template = Prefix + model.Template + FileExtension;
            var replacers = model.GetReplacers();

            using (var reader = new StreamReader(Resources.LoadFile(template)))
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