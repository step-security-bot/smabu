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
        public int TotalInvoices { get; internal set; }
        public int TotalOffers { get; internal set; }
        public int TotalCustomers { get; internal set; }
        public decimal TotalSalesVolume { get; internal set; }
    }
}