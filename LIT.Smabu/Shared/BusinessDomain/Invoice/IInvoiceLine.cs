using LIT.Smabu.Shared.BusinessDomain.Product;
using LIT.Smabu.Shared.Common;

namespace LIT.Smabu.Shared.BusinessDomain.Invoice
{
    public interface IInvoiceLine : IEntity<InvoiceLineId>
    {
        InvoiceId InvoiceId { get; }
        int Position { get; }
        Quantity Quantity { get; }
        string Details { get; }
        decimal UnitPrice { get; }
        decimal TotalPrice { get; }
        Currency Currency { get; }
        ProductId? ProductId { get; }
    }
}