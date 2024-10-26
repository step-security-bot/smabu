using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.Domain.OfferAggregate;
using LIT.Smabu.Domain.Shared;
using LIT.Smabu.Shared;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Orders.Get
{
    public class GetOrderHandler(IAggregateStore store) : IQueryHandler<GetOrderQuery, OrderDTO>
    {
        public async Task<Result<OrderDTO>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var order = await store.GetByAsync(request.Id);
            var customer = await store.GetByAsync(order.CustomerId);

            List<Invoice> invoices = order.InvoiceIds.Any()
                ? (await store.GetByAsync(order.InvoiceIds)).Select(x => x.Value).ToList()
                : [];

            List<Offer> offers = order.OfferIds.Any()
                ? (await store.GetByAsync(order.OfferIds)).Select(x => x.Value).ToList()
                : [];

            var invoicesReferences = invoices.Select(x => OrderReferenceItem<InvoiceId>.Create(x.Id, x.Number.Long, x.InvoiceDate, x.Amount)).ToList();
            var offersReferences = offers.Select(x => OrderReferenceItem<OfferId>.Create(x.Id, x.Number.Long, x.OfferDate, x.Amount)).ToList();

            return OrderDTO.Create(order, customer, offersReferences, invoicesReferences);
        }
    }
}
