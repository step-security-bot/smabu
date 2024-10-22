using LIT.Smabu.Domain.Common;
using LIT.Smabu.Domain.Services;
using LIT.Smabu.Domain.Shared;
using LIT.Smabu.UseCases.Shared;
using static LIT.Smabu.UseCases.Dashboards.Sales.GetSalesDashboardReadModel;

namespace LIT.Smabu.UseCases.Dashboards.Sales
{
    public class GetSalesDashboardHandler(SalesStatisticsService salesStatisticsService) : IQueryHandler<GetSalesDashboardQuery, GetSalesDashboardReadModel>
    {
        public async Task<Result<GetSalesDashboardReadModel>> Handle(GetSalesDashboardQuery request, CancellationToken cancellationToken)
        {
            GetSalesDashboardReadModel result = new()
            {
                ThisYear = DateTime.Now.Year,
                LastYear = DateTime.Now.Year - 1,
                Currency = Currency.EUR
            };

            await SetCustomerRankingsAsync(result);
            await SetSalesVolumesAsync(result);

            return Result<GetSalesDashboardReadModel>.Success(result);
        }

        private async Task SetSalesVolumesAsync(GetSalesDashboardReadModel result)
        {
            result.SalesVolumeThisYear = await salesStatisticsService.CalculateSalesVolumeForYearAsync(result.ThisYear);
            result.SalesVolumeLastYear = await salesStatisticsService.CalculateSalesVolumeForYearAsync(result.LastYear);
            result.SalesVolumeLast12Month = await salesStatisticsService.CalculateSalesVolumeForLastMonthsAsync(12);
            result.SalesVolumeLast24Month = await salesStatisticsService.CalculateSalesVolumeForLastMonthsAsync(24);
            result.SalesVolumeLast36Month = await salesStatisticsService.CalculateSalesVolumeForLastMonthsAsync(36);
            result.TotalSalesVolume = await salesStatisticsService.CalculateTotalSalesVolumeAsync();
            result.Top3InvoicesEver = (await salesStatisticsService.GetHighestInvoicesAsync(3))
                .Select(x => new TopItem(x.Number.ToString(), x.Amount)).ToList();
        }

        private async Task SetCustomerRankingsAsync(GetSalesDashboardReadModel result)
        {
            result.Top3CustomersEver = (await salesStatisticsService.GetCustomersWithHeighestAmountAsync(3))
                .Select(x => new TopItem(x.Key.Name, x.Value)).ToList();

            result.Top3CustomersThisYear = (await salesStatisticsService.GetCustomersWithHeighestAmountForYearAsync(result.ThisYear, 3))
                .Select(x => new TopItem(x.Key.Name, x.Value)).ToList();

            result.Top3CustomersLastYear = (await salesStatisticsService.GetCustomersWithHeighestAmountForYearAsync(result.LastYear, 3))
                .Select(x => new TopItem(x.Key.Name, x.Value)).ToList();

            result.Top3CustomersLast12Month = (await salesStatisticsService.GetCustomersWithHeighestAmountLastMonthsAsync(12, 3))
                .Select(x => new TopItem(x.Key.Name, x.Value)).ToList();
        }
    }
}
