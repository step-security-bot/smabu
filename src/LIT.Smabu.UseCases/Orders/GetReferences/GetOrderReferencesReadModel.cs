using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.Domain.OfferAggregate;

namespace LIT.Smabu.UseCases.Orders.GetReferences
{
    public record GetOrderReferencesReadModel(OrderReferenceDTO<OfferId>[] Offers, OrderReferenceDTO<InvoiceId>[] Invoices)
    {
        public decimal OfferAmount => Offers.Where(x => x.IsSelected ?? false).Sum(x => x.Amount ?? 0);
        public decimal InvoiceAmount => Invoices.Where(x => x.IsSelected ?? false).Sum(x => x.Amount ?? 0);
    }
}
