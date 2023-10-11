using LIT.Smabu.Domain.Shared.Common;
using LIT.Smabu.Domain.Shared.Invoices;
using LIT.Smabu.Infrastructure.Shared.Contracts;
using LIT.Smabu.Shared.Dashboards;
using MediatR;

namespace LIT.Smabu.Business.Service.Dashboards
{
    public class GetHomeDashboardHandler : IRequestHandler<GetHomeDashboardQuery, HomeDashboardDTO>
    {
        private readonly IAggregateStore aggregateStore;

        public GetHomeDashboardHandler(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }
        public async Task<HomeDashboardDTO> Handle(GetHomeDashboardQuery request, CancellationToken cancellationToken)
        {
            var currentYear = DateTime.Now.Year;
            var lastYear = DateTime.Now.Year - 1;
            var invoices = await this.aggregateStore.GetAllAsync<Invoice>();
            var salesPerCustomer = invoices.GroupBy(x => x.CustomerId)
                .OrderByDescending(x => x.Sum(y => y.Amount))
                .Take(3)
                .Select(x => new { CustomerId = x.Key, Sales = x.Sum(y => y.Amount) })
                .ToList();
            var customerIds = salesPerCustomer.Select(x => x.CustomerId).ToList();
            var customers = await this.aggregateStore.GetByAsync(customerIds);

            var totalSales = invoices.Sum(x => x.Amount);
            var salesCurrentYear = invoices.Where(x => x.FiscalYear == currentYear).Sum(x => x.Amount);
            var salesLastYear = invoices.Where(x => x.FiscalYear == lastYear).Sum(x => x.Amount);
            var sales3Month = invoices.Where(x => x.SalesReportDate >= DateOnly.FromDateTime(DateTime.Now.AddMonths(-3))).Sum(x => x.Amount);
            var sales6Month = invoices.Where(x => x.SalesReportDate >= DateOnly.FromDateTime(DateTime.Now.AddMonths(-6))).Sum(x => x.Amount);
            var sales12Month = invoices.Where(x => x.SalesReportDate >= DateOnly.FromDateTime(DateTime.Now.AddMonths(-12))).Sum(x => x.Amount);
            var result = new HomeDashboardDTO()
            {
                BestSalesCustomer1Name = salesPerCustomer[0] != null ? customers[salesPerCustomer[0].CustomerId].Name : "",
                BestSalesCustomer1Value = salesPerCustomer[0] != null ? Currency.GetEuro().Format(salesPerCustomer[0].Sales) : "",
                BestSalesCustomer2Name = salesPerCustomer.Count >= 1 ? customers[salesPerCustomer[1].CustomerId].Name : "",
                BestSalesCustomer2Value = salesPerCustomer.Count >= 1 ? Currency.GetEuro().Format(salesPerCustomer[1].Sales) : "",
                BestSalesCustomer3Name = salesPerCustomer.Count >= 2 ? customers[salesPerCustomer[2].CustomerId].Name : "",
                BestSalesCustomer3Value = salesPerCustomer.Count >= 2 ? Currency.GetEuro().Format(salesPerCustomer[2].Sales) : "",

                TotalSales = Currency.GetEuro().Format(totalSales),
                SalesCurrentYear = Currency.GetEuro().Format(salesCurrentYear),
                SalesLastYear = Currency.GetEuro().Format(salesLastYear),

                Sales3Month = Currency.GetEuro().Format(sales3Month),
                Sales6Month = Currency.GetEuro().Format(sales6Month),
                Sales12Month = Currency.GetEuro().Format(sales12Month),
            };
            return result;
        }
    }
}
