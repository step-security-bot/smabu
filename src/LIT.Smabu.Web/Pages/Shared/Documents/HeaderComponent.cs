using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace LIT.Smabu.Web.Pages.Shared.Documents
{

    public class HeaderComponent(string documentType) : IComponent
    {
        public void Compose(IContainer container)
        {
            var titleStyleWhite = TextStyle.Default.FontSize(20).ExtraBold().FontColor(Colors.White);
            var titleStyleDefault = TextStyle.Default.FontSize(10).FontColor(Constants.AccentColor1);
            var titleStyleBold = TextStyle.Default.FontSize(20).ExtraBold().FontColor(Constants.AccentColor1Light);

            container.BorderBottom(1).BorderColor(Constants.AccentColor1Light).Row(row =>
            {
                row.AutoItem().Background(Constants.AccentColor1Light).PaddingVertical(6).PaddingHorizontal(Constants.PaddingMedium).Column(column =>
                {
                    column.Item().Text("LIT").Style(titleStyleWhite); ;
                });
                row.RelativeItem().PaddingLeft(Constants.PaddingMedium).AlignMiddle().Column(column =>
                {
                    column.Item().Text($"LennipsIT | Web- und Softwareentwicklung").Style(titleStyleDefault.Weight(FontWeight.SemiBold));
                    column.Item().Text($"iSAQB® Certified Professional for Software Architecture").Style(titleStyleDefault);
                });
                row.AutoItem().AlignMiddle().Column(column =>
                {
                    column.Item().Text(documentType).Style(titleStyleBold); ;
                });
            });
        }
    }
}
