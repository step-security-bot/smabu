using LIT.Smabu.Business.Service.Contratcs;
using LIT.Smabu.Domain.Shared.Contracts;
using LIT.Smabu.Domain.Shared.Invoices;
using LIT.Smabu.Infrastructure.Shared.Contracts;
using LIT.Smabu.Shared.Invoices;

namespace LIT.Smabu.Business.Service.Invoices.Mappings
{
    public class InvoiceMapper : IMapperManyAsync<Invoice, InvoiceDTO>, IMapper<InvoiceLine, InvoiceLineDTO>
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
                    Tax = item.Tax,
                    TaxDetails = item.TaxDetails ?? "",
                    InvoiceLines = item.InvoiceLines.Select(x => Map(x)).ToList()
                });
            }
            return result;
        }

        public async Task<InvoiceDTO> MapAsync(Invoice source)
        {
            var customer = await this.aggregateStore.GetByAsync(source.CustomerId);
            var customerDto = await new CustomerMapper(this.aggregateStore).MapAsync(customer);

            return new()
            {
                Id = source.Id,
                Customer = customerDto,
                Number = source.Number,
                Amount = source.Amount,
                PerformancePeriod = source.PerformancePeriod,
                FiscalYear = source.FiscalYear,
                Currency = source.Currency,
                Tax = source.Tax,
                TaxDetails = source.TaxDetails ?? "",
                InvoiceLines = source.InvoiceLines.Select(x => Map(x)).ToList()
            };
        }

        public InvoiceLineDTO Map(InvoiceLine source)
        {
            return new InvoiceLineDTO()
            {
                Id = source.Id,
                Details = source.Details,
                InvoiceId = source.InvoiceId,
                Position = source.Position,
                Quantity = source.Quantity,
                UnitPrice = source.UnitPrice,
                TotalPrice = source.TotalPrice,
                ProductId = source.ProductId,
            };
        }
    }
}
