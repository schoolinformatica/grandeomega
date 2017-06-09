namespace Highcharts
{
    public sealed class TemplateFile
    {
        public static readonly TemplateFile Dataseries = new TemplateFile("Highcharts.Files.dataseries.tmpl");
        public static readonly TemplateFile Chart = new TemplateFile("Highcharts.Files.scatterplot.tmpl");
        
        public string Value { get; }
        private TemplateFile(string value) => Value = value;
    }
}