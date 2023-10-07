using LIT.Smabu.Business.Service.Contratcs;
using LIT.Smabu.Domain.Shared.Contracts;
using LIT.Smabu.Domain.Shared.Customers;
using LIT.Smabu.Domain.Shared.Invoices;
using LIT.Smabu.Infrastructure.Shared.Contracts;
using LIT.Smabu.Shared.Invoices;

namespace LIT.Smabu.Business.Service.Invoices.Mappings
{
    public class InvoiceMapper : IMapperManyAsync<Invoice, InvoiceDTO>
    {
        private readonly IAggregateStore aggregateStore;

        public InvoiceMapper(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

        public async Task<Dictionary<IEntityId, InvoiceDTO>> MapAsync(IEnumerable<Invoice> source)
        {
            var result = new Dictionary<IEntityId, InvoiceDTO>();
            var customerIds = source.Select(x => x.CustomerId).ToList();
            var customers = await this.aggregateStore.GetByAsync(customerIds);
            var customerDtos = await new CustomerMapper(this.aggregateStore).MapAsync(customers.Values);
            foreach (var item in source)
            {
                result.Add(item.Id, new()
                {
                    Id = item.Id,
                    Customer = customerDtos[item.CustomerId],
                    Number = item.Number,
                    Amount = item.Amount,
                    PerformancePeriod = item.PerformancePeriod,
                    FiscalYear = item.FiscalYear,
                    Currency = item.Currency,
                    InvoiceLines = null //item.InvoiceLines
                });
            }
            return result;
        }

        public Task<InvoiceDTO> MapAsync(Invoice source)
        {
            throw new NotImplementedException();
        }
    }
}
