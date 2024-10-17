using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.Shared;
using LIT.Smabu.UseCases.SeedWork;

namespace LIT.Smabu.UseCases.Invoices.List
{
    public class ListInvoicesHandler(IAggregateStore aggregateStore) : IQueryHandler<ListInvoicesQuery, InvoiceDTO[]>
    {
        public async Task<Result<InvoiceDTO[]>> Handle(ListInvoicesQuery request, CancellationToken cancellationToken)
        {
            var invoices = await aggregateStore.GetAllAsync<Invoice>();
            var customerIds = invoices.Select(x => x.CustomerId).ToList();
            var customers = await aggregateStore.GetByAsync(customerIds);
            var result = invoices.Select(x => InvoiceDTO.Create(x, customers[x.CustomerId]))
                .OrderBy(x => x.Number.IsTemporary ? 0 : 1)
                .ThenByDescending(x => x.Number)
                .ThenByDescending(x => x.CreatedOn)
                .ToArray();
            return result;
        }
    }
}
