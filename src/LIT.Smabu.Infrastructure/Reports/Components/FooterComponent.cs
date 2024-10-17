using LIT.Smabu.Infrastructure.Reports;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace LIT.Smabu.Infrastructure.Reports.Components
{

    public class FooterComponent(ReportsConfig config) : IComponent
    {
        public void Compose(IContainer container)
        {
            container.Column(column =>
            {
                column.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(3);
                        columns.RelativeColumn(4);
                        columns.RelativeColumn(4);
                        columns.RelativeColumn(2);
                    });

                    table.Cell().Row(1).Column(1).Element(CellStyle1).Text(config.Footer.Column1.Title);
                    table.Cell().Row(2).Column(1).Element(CellStyle2).Text(config.Footer.Column1.Line1).FontSize(8).Light();
                    table.Cell().Row(3).Column(1).Element(CellStyle2).Text(config.Footer.Column1.Line2).FontSize(8).Light();
                    table.Cell().Row(4).Column(1).Element(CellStyle2).PaddingBottom(10).Text(config.Footer.Column1.Line3).FontSize(8).Light();

                    table.Cell().Row(1).Column(2).Element(CellStyle1).Text(config.Footer.Column2.Title);
                    table.Cell().Row(2).Column(2).Element(CellStyle2).Text(config.Footer.Column2.Line1).FontSize(8).Light();
                    table.Cell().Row(3).Column(2).Element(CellStyle2).Text(config.Footer.Column2.Line2).FontSize(8).Light();
                    table.Cell().Row(4).Column(2).Element(CellStyle2).PaddingBottom(10).Text(config.Footer.Column2.Line3).FontSize(8).Light();

                    table.Cell().Row(1).Column(3).Element(CellStyle1).Text(config.Footer.Column3.Title);
                    table.Cell().Row(2).Column(3).Element(CellStyle2).Text(config.Footer.Column3.Line1).FontSize(8).Light();
                    table.Cell().Row(3).Column(3).Element(CellStyle2).Text(config.Footer.Column3.Line2).FontSize(8).Light();
                    table.Cell().Row(4).Column(3).Element(CellStyle2).PaddingBottom(10).Text(config.Footer.Column3.Line3).FontSize(8).Light();

                    table.Cell().Row(1).RowSpan(4).Column(4).Element(CellStyle1).PaddingRight(20).PaddingTop(5).AlignRight().Text("LIT")
                        .FontSize(25).FontColor(Colors.Grey.Lighten3).ExtraBold();


                    table.Cell().Row(5).Column(1).ColumnSpan(3).Element(CellStyle3).Text("Erstellt durch LIT|smabu").FontColor(Constants.AccentColor1Light);
                    table.Cell().Row(5).Column(4).Element(CellStyle3).DefaultTextStyle(Constants.DefaultTextStyle.FontColor(Constants.AccentColor1Light)).Text(x =>
                    {
                        x.AlignRight();
                        x.CurrentPageNumber();
                        x.Span("/");
                        x.TotalPages();
                    });

                    IContainer CellStyle1(IContainer container) => container.Background(Colors.Grey.Lighten4).PaddingTop(10).PaddingLeft(10);
                    IContainer CellStyle2(IContainer container) => container.Background(Colors.Grey.Lighten4).PaddingHorizontal(10);
                    IContainer CellStyle3(IContainer container) => container.BorderTop(2).BorderColor(Constants.AccentColor1Light).PaddingVertical(2);
                });
            });
        }
    }
}
