using LIT.Smabu.Domain.Shared.Customers;
using LIT.Smabu.Domain.Shared.Invoices;
using LIT.Smabu.Infrastructure.Shared.Contracts;
using LIT.Smabu.Shared.Invoices;
using MediatR;

namespace LIT.Smabu.Business.Service.Invoices.Queries
{
    public class GetInvoicesHandler : IRequestHandler<GetInvoicesQuery, InvoiceDTO[]>
    {
        private readonly IAggregateStore aggregateStore;

        public GetInvoicesHandler(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

        public async Task<InvoiceDTO[]> Handle(GetInvoicesQuery request, CancellationToken cancellationToken)
        {
            var invoices = await aggregateStore.GetAsync<Invoice>();
            InvoiceDTO[] result = await MapResultAsync(invoices);
            return result;
        }

        private async Task<InvoiceDTO[]> MapResultAsync(List<Invoice> invoices)
        {
            var customerIds = invoices.Select(x => x.CustomerId).Distinct().ToList();
            var customers = await aggregateStore.GetAsync<Customer, CustomerId>(customerIds);
            var result = invoices
                .Select(x => InvoiceDTO.Map(x, customers[x.CustomerId]))
                .OrderByDescending(x => x.Number)
                .ToArray();
            return result;
        }
    }
}
