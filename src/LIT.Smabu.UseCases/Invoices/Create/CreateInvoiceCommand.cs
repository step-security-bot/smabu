using LIT.Smabu.Domain.Common;
using LIT.Smabu.Domain.CustomerAggregate;
using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.Domain.OfferAggregate;
using LIT.Smabu.Domain.OrderAggregate;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Invoices.Create
{
    public record CreateInvoiceCommand : ICommand<InvoiceId>
    {
        public required InvoiceId Id { get; set; }
        public required CustomerId CustomerId { get; set; }
        public required int FiscalYear { get; set; }
        public required Currency Currency { get; set; }
        public DatePeriod? PerformancePeriod { get; set; }
        public TaxRate? TaxRate { get; set; }
        public OrderId? OrderId { get; set; }
        public OfferId? OfferId { get; set; }
    }
}
