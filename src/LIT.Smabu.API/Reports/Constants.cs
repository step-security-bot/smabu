using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace LIT.Smabu.API.Reports
{
    public static class Constants
    {
        // Colors
        public static readonly TextStyle DefaultTextStyle = TextStyle.Default.FontSize(10).FontColor(Colors.Grey.Darken4);
        public static readonly string AccentColor1Light = Colors.LightBlue.Lighten2;
        public static readonly string AccentColor1 = Colors.LightBlue.Medium;

        // Measurements
        public static readonly float PaddingSmall = 5;
        public static readonly float PaddingMedium = 10;
        public static readonly float PaddingLarge = 20;
        internal static readonly float PageMargin = 30;
    }
}
