namespace Highcharts
{
    
    /**********************************************
     *
     * Here we used the Type-Safe Enum Pattern. A
     * Normal enum couldn't be used, because the
     * string value is needed. 
     *
     **********************************************/
    
    public sealed class Highchart
    {
        public static readonly Highchart Scatterplot = new Highchart("scatter");
        public static readonly Highchart Regression = new Highchart("line");

        public string Value { get; }
        private Highchart(string value) => Value = value;
    }
}