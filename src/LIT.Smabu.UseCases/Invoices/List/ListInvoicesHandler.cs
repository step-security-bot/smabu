using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.Shared.Interfaces;
using MediatR;

namespace LIT.Smabu.UseCases.Invoices.List
{
    public class ListInvoicesHandler : IRequestHandler<ListInvoicesQuery, InvoiceDTO[]>
    {
        private readonly IAggregateStore aggregateStore;

        public ListInvoicesHandler(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

        public async Task<InvoiceDTO[]> Handle(ListInvoicesQuery request, CancellationToken cancellationToken)
        {
            var invoices = await aggregateStore.GetAllAsync<Invoice>();
            var customerIds = invoices.Select(x => x.CustomerId).Distinct().ToList();
            var customers = await aggregateStore.GetByAsync(customerIds);
            var result = invoices.Select(x => InvoiceDTO.From(x, customers.GetValueOrDefault(x.CustomerId)!))
                .OrderByDescending(x => x.Number)
                .ToArray();
            return result;
        }
    }
}
