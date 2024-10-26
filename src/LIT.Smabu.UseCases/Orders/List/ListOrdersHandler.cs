using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.Domain.OfferAggregate;
using LIT.Smabu.Domain.OrderAggregate;
using LIT.Smabu.Domain.Shared;
using LIT.Smabu.Shared;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Orders.List
{
    public class ListOrdersHandler(IAggregateStore store) : IQueryHandler<ListOrdersQuery, OrderDTO[]>
    {
        public async Task<Result<OrderDTO[]>> Handle(ListOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await store.GetAllAsync<Order>();
            var customers = await store.GetByAsync(orders.Select(x => x.CustomerId).Distinct());
            var invoiceIds = orders.SelectMany(x => x.InvoiceIds).Distinct();
            var offerIds = orders.SelectMany(x => x.OfferIds).Distinct();

            var invoices = invoiceIds.Any()
                ? (await store.GetByAsync(invoiceIds))
                : [];

            var offers = offerIds.Any()
                ? (await store.GetByAsync(offerIds))
                : [];

            var invoicesReferences = invoices.Values.Select(x => OrderReferenceItem<InvoiceId>.Create(x.Id, x.Number.Long, x.InvoiceDate, x.Amount)).ToList();
            var offersReferences = offers.Values.Select(x => OrderReferenceItem<OfferId>.Create(x.Id, x.Number.Long, x.OfferDate, x.Amount)).ToList();

            return orders.Select
                (
                    x => OrderDTO.Create(x, customers[x.CustomerId],
                    offersReferences.Where(o => x.OfferIds.Contains(o.Id)).ToList(),
                    invoicesReferences.Where(i => x.InvoiceIds.Contains(i.Id)).ToList()
                ))
                .OrderByDescending(x => x.Number).ToArray();
        }
    }
}
