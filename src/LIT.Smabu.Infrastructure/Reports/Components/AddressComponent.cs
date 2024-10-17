using LIT.Smabu.UseCases.Common;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace LIT.Smabu.Infrastructure.Reports.Components
{
    public class AddressComponent(ReportsConfig config, AddressDTO address) : IComponent
    {
        public void Compose(IContainer container)
        {
            container.Column(column =>
            {
                column.Spacing(0);
                column.Item().PaddingBottom(Constants.PaddingSmall).Text(config.SenderAddress).Light().FontSize(8);
                column.Item().DefaultTextStyle(Constants.DefaultTextStyle).Text(address.Name1);
                column.Item().DefaultTextStyle(Constants.DefaultTextStyle).Text(address.Name2);
                column.Item().DefaultTextStyle(Constants.DefaultTextStyle).Text($"{address.Street} {address.HouseNumber}");
                column.Item().DefaultTextStyle(Constants.DefaultTextStyle).Text($"{address.PostalCode} {address.City}");
            });
        }
    }
}
