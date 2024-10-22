using LIT.Smabu.Domain.Common;
using LIT.Smabu.Domain.CustomerAggregate;
using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.Domain.OfferAggregate;
using LIT.Smabu.Domain.Services;
using LIT.Smabu.Domain.Shared;
using LIT.Smabu.Shared;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Dashboards.Welcome
{
    public class GetWelcomeDashboardHandler(SalesStatisticsService salesStatisticsService, IAggregateStore aggregateStore) : IQueryHandler<GetWelcomeDashboardQuery, GetWelcomeDashboardReadModel>
    {
        public async Task<Result<GetWelcomeDashboardReadModel>> Handle(GetWelcomeDashboardQuery request, CancellationToken cancellationToken)
        {
            var invoicesCount = await aggregateStore.CountAsync<Invoice>();
            var offersCount = await aggregateStore.CountAsync<Offer>();
            var customersCount = await aggregateStore.CountAsync<Customer>();

            GetWelcomeDashboardReadModel result = new()
            {
                ThisYear = DateTime.Now.Year,
                LastYear = DateTime.Now.Year - 1,
                Currency = Currency.EUR,
                TotalInvoices = invoicesCount,
                TotalOffers = offersCount,
                TotalCustomers = customersCount
            };

            await SetSalesVolumesAsync(result);


            return Result<GetWelcomeDashboardReadModel>.Success(result);
        }

        private async Task SetSalesVolumesAsync(GetWelcomeDashboardReadModel result)
        {
            result.SalesVolumeThisYear = await salesStatisticsService.CalculateSalesVolumeForYearAsync(result.ThisYear);
            result.SalesVolumeLastYear = await salesStatisticsService.CalculateSalesVolumeForYearAsync(result.LastYear);
            result.TotalSalesVolume = await salesStatisticsService.CalculateTotalSalesVolumeAsync();
        }
    }
}
