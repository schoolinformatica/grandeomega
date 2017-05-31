namespace Highcharts
{
    
    /********************************************************************
    * 
    * Here we used the Type-Safe Enum Patter. Because a normal 
    * Enumeration is not capable of holding string values, which we need 
    * in this case.
    *
    *********************************************************************/
    
    
    public sealed class Replacer
    {   
        public static readonly Replacer DivId = new Replacer("divid");
        public static Replacer Chart = new Replacer("chart");
        public static Replacer Title = new Replacer("title");
        public static Replacer Subtitle = new Replacer("subtitle");
        public static Replacer Xlabel = new Replacer("xlabel");
        public static Replacer Ylabel = new Replacer("ylabel");
        public static Replacer Xtooltip = new Replacer("xtooltip");
        public static Replacer Ytooltip = new Replacer("ytooltip");
        public static Replacer Data = new Replacer("data");
        public static Replacer Type = new Replacer("type");
        public static Replacer Name = new Replacer("name");
        public static Replacer Marker = new Replacer("marker");
        public static Replacer Mousetracking = new Replacer("mousetracking");

        public string Value { get; }

        private Replacer(string value) => Value = value;

    }
    
}