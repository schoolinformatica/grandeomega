namespace Highcharts
{

    public sealed class Replacer
    {
        public static readonly Replacer DivId = new Replacer("divid");
        public static readonly Replacer Chart = new Replacer("chart");
        public static readonly Replacer Title = new Replacer("title");
        public static readonly Replacer Subtitle = new Replacer("subtitle");
        public static readonly Replacer Xlabel = new Replacer("xlabel");
        public static readonly Replacer Ylabel = new Replacer("ylabel");
        public static readonly Replacer Xtooltip = new Replacer("xtooltip");
        public static readonly Replacer Ytooltip = new Replacer("ytooltip");
        public static readonly Replacer Data = new Replacer("data");
        public static readonly Replacer Type = new Replacer("type");
        public static readonly Replacer Name = new Replacer("name");
        public static readonly Replacer Marker = new Replacer("marker");
        public static readonly Replacer Mousetracking = new Replacer("mousetracking");

        public string Value { get; }

        private Replacer(string value) => Value = value;
    }
}