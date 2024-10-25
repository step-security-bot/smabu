using LIT.Smabu.Domain.Common;

namespace LIT.Smabu.UseCases.Dashboards.Welcome
{
    public record GetWelcomeDashboardReadModel
    {
        public int ThisYear { get; internal set; }
        public int LastYear { get; internal set; }
        public Currency Currency { get; internal set; } = Currency.EUR;
        public decimal SalesVolumeThisYear { get; internal set; }
        public decimal SalesVolumeLastYear { get; internal set; }
        public int InvoiceCount { get; internal set; }
        public int OfferCount { get; internal set; }
        public int CustomerCount { get; internal set; }
        public decimal TotalSalesVolume { get; internal set; }
        public DateTime Version { get; internal set; }
    }
}