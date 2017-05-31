using System.IO;
using System.Reflection;

namespace Highcharts
{
    public class Resources
    {
        private static readonly Assembly Assembly = typeof(Highchart).GetTypeInfo().Assembly;

        public static Stream LoadFile(string resource) => Assembly.GetManifestResourceStream(resource);

        public static string[] GetResourcesNames() => Assembly.GetManifestResourceNames();
    }
}