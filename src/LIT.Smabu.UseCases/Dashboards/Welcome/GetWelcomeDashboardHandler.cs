using LIT.Smabu.Domain.Common;
using LIT.Smabu.Domain.Services;
using LIT.Smabu.Domain.Shared;
using LIT.Smabu.Shared;
using LIT.Smabu.UseCases.Shared;
using static LIT.Smabu.UseCases.Dashboards.Welcome.GetWelcomeDashboardReadModel;

namespace LIT.Smabu.UseCases.Dashboards.Welcome
{
    public class GetWelcomeDashboardHandler(SalesStatisticsService salesStatisticsService) : IQueryHandler<GetWelcomeDashboardQuery, GetWelcomeDashboardReadModel>
    {
        public async Task<Result<GetWelcomeDashboardReadModel>> Handle(GetWelcomeDashboardQuery request, CancellationToken cancellationToken)
        {
            GetWelcomeDashboardReadModel result = new()
            {
                ThisYear = DateTime.Now.Year,
                LastYear = DateTime.Now.Year - 1,
                Currency = Currency.EUR
            };

            await SetCustomerRankingsAsync(result);
            await SetSalesVolumesAsync(result);

            return Result<GetWelcomeDashboardReadModel>.Success(result);
        }

        private async Task SetSalesVolumesAsync(GetWelcomeDashboardReadModel result)
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

        private async Task SetCustomerRankingsAsync(GetWelcomeDashboardReadModel result)
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
