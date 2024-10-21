using LIT.Smabu.Domain.CustomerAggregate;
using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.Shared;

namespace LIT.Smabu.Domain.Services
{
    public class SalesStatisticsService(IAggregateStore aggregateStore)
    {
        private IReadOnlyList<Invoice>? invoices;
        private IReadOnlyList<Customer>? customers;

        public async Task<decimal> CalculateSalesVolumeForYearAsync(int fiscalYear)
        {
            return (await GetInvoicesAsync())
                .Where(x => x.FiscalYear == fiscalYear)
                .Sum(x => x.Amount);
        }

        public async Task<decimal> CalculateSalesVolumeForLastMonthsAsync(uint monthsBack)
        {
            return (await GetInvoicesAsync())
                .Where(x => x.Meta!.CreatedOn >= DateTime.Now.AddMonths((int)monthsBack * -1))
                .Sum(x => x.Amount);
        }

        public async Task<decimal> CalculateTotalSalesVolumeAsync()
        {
            return (await GetInvoicesAsync())
                .Sum(x => x.Amount);
        }

        public async Task<Invoice[]> GetHighestInvoicesAsync(int limit = 3)
        {
            return (await GetInvoicesAsync())
                .OrderByDescending(x => x.Amount)
                .Take(limit)
                .ToArray();
        }

        public async Task<decimal> CalculateSalesVolumeForCustomerAsync(CustomerId customerId)
        {
            return (await GetInvoicesAsync())
                .Where(x => x.CustomerId == customerId)
                .Sum(x => x.Amount);
        }

        public async Task<Dictionary<Customer, decimal>> GetCustomersWithHeighestAmountAsync(int limit = 3)
        {
            var customers = await GetCustomerAsync();
            var result = (await GetInvoicesAsync())
                .GroupBy(x => x.CustomerId)
                .Select(x => new
                {
                    Customer = customers.Single(y => y.Id == x.Key),
                    TotalAmount = x.Sum(i => i.Amount)
                })
                .OrderByDescending(x => x.TotalAmount)
                .Take(limit)
                .ToList()
                .ToDictionary(x => x.Customer, x => x.TotalAmount);
            return result;
        }

        public async Task<Dictionary<Customer, decimal>> GetCustomersWithHeighestAmountForYearAsync(int fiscalYear, int limit = 3)
        {
            var customers = await GetCustomerAsync();
            var result = (await GetInvoicesAsync())
                .Where(x => x.FiscalYear == fiscalYear)
                .GroupBy(x => x.CustomerId)
                .Select(x => new
                {
                    Customer = customers.Single(y => y.Id == x.Key),
                    TotalAmount = x.Sum(i => i.Amount)
                })
                .OrderByDescending(x => x.TotalAmount)
                .Take(limit)
                .ToList()
                .ToDictionary(x => x.Customer, x => x.TotalAmount);
            return result;
        }

        public async Task<Dictionary<Customer, decimal>> GetCustomersWithHeighestAmountLastMonthsAsync(uint monthsBack, int limit = 3)
        {
            var customers = await GetCustomerAsync();
            var result = (await GetInvoicesAsync())
                .Where(x => x.Meta!.CreatedOn >= DateTime.Now.AddMonths((int)monthsBack * -1))
                .GroupBy(x => x.CustomerId)
                .Select(x => new
                {
                    Customer = customers.Single(y => y.Id == x.Key),
                    TotalAmount = x.Sum(i => i.Amount)
                })
                .OrderByDescending(x => x.TotalAmount)
                .Take(limit)
                .ToList()
                .ToDictionary(x => x.Customer, x => x.TotalAmount);
            return result;
        }

        private async Task<IReadOnlyList<Invoice>> GetInvoicesAsync() => invoices ??= await aggregateStore.GetAllAsync<Invoice>();
        private async Task<IReadOnlyList<Customer>> GetCustomerAsync() => customers ??= await aggregateStore.GetAllAsync<Customer>();

    }
}
