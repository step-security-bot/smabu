using LIT.Smabu.Domain.Common;
using LIT.Smabu.Domain.Services;
using LIT.Smabu.Domain.Shared;
using LIT.Smabu.UseCases.Shared;

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

            await SetSalesVolumesAsync(result);

            return Result<GetWelcomeDashboardReadModel>.Success(result);
        }

        private async Task SetSalesVolumesAsync(GetWelcomeDashboardReadModel result)
        {
            result.SalesVolumeThisYear = await salesStatisticsService.CalculateSalesVolumeForYearAsync(result.ThisYear);
            result.SalesVolumeLastYear = await salesStatisticsService.CalculateSalesVolumeForYearAsync(result.LastYear);
        }
    }
}
