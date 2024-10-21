using LIT.Smabu.Domain.Common;

namespace LIT.Smabu.UseCases.Dashboards.Welcome
{
    public record GetWelcomeDashboardReadModel
    {
        public int ThisYear { get; internal set; }
        public int LastYear { get; internal set; }
        public Currency Currency { get; internal set; } = Currency.EUR;

        public List<TopItem> Top3InvoicesEver { get; internal set; } = [];
        public List<TopItem> Top3CustomersEver { get; internal set; } = [];
        public List<TopItem> Top3CustomersThisYear { get; internal set; } = [];
        public List<TopItem> Top3CustomersLastYear { get; internal set; } = [];
        public List<TopItem> Top3CustomersLast12Month { get; internal set; } = [];
        public decimal SalesVolumeThisYear { get; internal set; }
        public decimal SalesVolumeLastYear { get; internal set; }
        public decimal SalesVolumeLast12Month { get; internal set; }
        public decimal SalesVolumeLast24Month { get; internal set; }
        public decimal SalesVolumeLast36Month { get; internal set; }
        public decimal TotalSalesVolume { get; internal set; }

        public record TopItem(string Name, decimal Total);
    }
}