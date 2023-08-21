using LIT.Smabu.Shared.BusinessDomain.Customer;
using LIT.Smabu.Shared.BusinessDomain.Offer;
using LIT.Smabu.Shared.BusinessDomain.Order;
using LIT.Smabu.Shared.Common;

namespace LIT.Smabu.Shared.BusinessDomain.Invoice
{
    public interface IInvoice : IEntity<InvoiceId>
    {
        CustomerId CustomerId { get; }
        int FiscalYear { get; }
        Period PerformancePeriod { get; }
        decimal Tax { get; }
        string TaxDetails { get; }
        OrderId? OrderId { get; }
        OfferId? OfferId { get; }
        List<IInvoiceLine> InvoiceLines { get; }
    }
}