using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Templatengine
{
    public static class TmplEngine
    {
        private static readonly string Path = Directory.GetCurrentDirectory() + "/wwwroot/templates/";
        private const string FileExtension = ".tmpl";

        public static string CreateTemplate(ITmplModel model)
        {
            string bodyFile;

            var template = Path + model.Template + FileExtension;
            var replacers = model.GetReplacers();

            using (var reader = new StreamReader(File.Open(template, FileMode.Open)))
            {
                bodyFile = reader.ReadToEnd();

                for (var i = 0; i < replacers.Count; i++)
                {
                    var replacer = replacers[i];
                    bodyFile = bodyFile.Replace("<%" + replacer.Item1 + "%>", replacer.Item2);
                }

            }

            return bodyFile;
        }
    }
}