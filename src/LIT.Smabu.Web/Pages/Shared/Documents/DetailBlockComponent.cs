using LIT.Smabu.Domain.Common;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace LIT.Smabu.Web.Pages.Shared.Documents
{
    public class InfoBlockComponent(string issueNumber, DateTime? issueDate, string customerNumber, DatePeriod performanceDate) : IComponent
    {
        public void Compose(IContainer container)
        {
            container.Column(column =>
            {
                column.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                    });

                    table.Cell().Row(1).Column(1).Element(CellStyle1).Text("Nummer").Light();
                    table.Cell().Row(1).Column(2).Element(CellStyle2).AlignRight().Text($"{issueNumber}").FontColor(Colors.Grey.Darken2).SemiBold();

                    table.Cell().Row(2).Column(1).Element(CellStyle1).Text("Datum").Light();
                    table.Cell().Row(2).Column(2).Element(CellStyle2).AlignRight().Text($"{issueDate:d}").FontColor(Colors.Grey.Darken2);

                    table.Cell().Row(3).Column(1).Element(CellStyle1).Text("Kunde").Light();
                    table.Cell().Row(3).Column(2).Element(CellStyle2).AlignRight().Text($"{customerNumber}").FontColor(Colors.Grey.Darken2);

                    table.Cell().Row(4).Column(1).Element(CellStyle1).Text("Leistungszeitraum").Light();
                    table.Cell().Row(4).Column(2).Element(CellStyle2)
                        .AlignRight().Text($"{performanceDate.ToStringInMonths()}").FontColor(Colors.Grey.Darken2);

                    IContainer CellStyle1(IContainer container) => container.BorderBottom(2).BorderColor(Colors.White).Padding(Constants.PaddingSmall).AlignRight();
                    IContainer CellStyle2(IContainer container) => container.BorderBottom(2).BorderColor(Colors.White).Background(Colors.Grey.Lighten3).Padding(5);
                });
            });
        }
    }
}
