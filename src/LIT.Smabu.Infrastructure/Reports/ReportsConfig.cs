namespace LIT.Smabu.Infrastructure.Reports
{
    public class ReportsConfig
    {
        public HeaderConfig Header { get; set; } = new HeaderConfig();
        public string SenderAddress { get; set; } = "";
        public FooterConfig Footer { get; set; } = new FooterConfig();
    }

    public class HeaderConfig
    {
        public string Title { get; set; } = "";
        public string Subtitle { get; set; } = "";
    }

    public class FooterConfig
    {
        public ColumnConfig Column1 { get; set; } = new ColumnConfig();
        public ColumnConfig Column2 { get; set; } = new ColumnConfig();
        public ColumnConfig Column3 { get; set; } = new ColumnConfig();
    }

    public class ColumnConfig
    {
        public string Title { get; set; } = "";
        public string Line1 { get; set; } = "";
        public string Line2 { get; set; } = "";
        public string Line3 { get; set; } = "";
    }
}
