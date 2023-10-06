using LIT.Smabu.Domain.Shared.Customers;
using LIT.Smabu.Domain.Shared.Invoices;
using LIT.Smabu.Infrastructure.CQRS;
using LIT.Smabu.Infrastructure.DDD;
using LIT.Smabu.Shared.Invoices;

namespace LIT.Smabu.Business.Service.Invoices.Queries
{
    public class GetInvoicesHandler : RequestHandler<GetInvoicesQuery, InvoiceDTO[]>
    {
        public GetInvoicesHandler(IAggregateStore aggregateStore) : base(aggregateStore)
        {
        }

        public override async Task<InvoiceDTO[]> Handle(GetInvoicesQuery request, CancellationToken cancellationToken)
        {
            var invoices = await AggregateStore.GetAllAsync<Invoice>();
            InvoiceDTO[] result = await MapResultAsync(invoices);
            return result;
        }

        private async Task<InvoiceDTO[]> MapResultAsync(List<Invoice> invoices)
        {
            var customerIds = invoices.Select(x => x.CustomerId).Distinct().ToList();
            var customers = await AggregateStore.GetByIdsAsync<Customer, CustomerId>(customerIds);
            var result = invoices
                .Select(x => InvoiceDTO.Map(x, customers[x.CustomerId]))
                .OrderByDescending(x => x.Number)
                .ToArray();
            return result;
        }
    }
}
