using LIT.Smabu.Domain.Shared.Customers;
using LIT.Smabu.Domain.Shared.Invoices;
using LIT.Smabu.Infrastructure.Shared.Contracts;

namespace LIT.Smabu.Business.Service.Customers.Common
{
    internal class CustomerTotalSalesCalculator
    {
        private readonly IAggregateStore aggregateStore;

        public CustomerTotalSalesCalculator(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

        public decimal TotalSales { get; private set; }
        public decimal SalesCurrentYear { get; private set; }
        public decimal SalesLastYear { get; private set; }

        public async Task StartAsync(CustomerId customerId)
        {
            var currentYear = DateTime.Now.Year;
            var lastYear = currentYear - 1;
            var invoices = (await this.aggregateStore.GetAsync<Invoice>())
                .Where(x => x.CustomerId == customerId).ToList();

            this.TotalSales = invoices.Sum(x => x.Amount);
            this.SalesCurrentYear = invoices.Where(x => x.FiscalYear == currentYear).Sum(x => x.Amount);
            this.SalesLastYear = invoices.Where(x => x.FiscalYear == lastYear).Sum(x => x.Amount);
        }
    }
}
