using LIT.Smabu.Shared.Domain.Common;
using LIT.Smabu.Shared.Domain.Invoices;
using LIT.Smabu.Shared.Domain.Offers;
using LIT.Smabu.Shared.Domain.Orders;
using MediatR;

namespace LIT.Smabu.Shared.Domain.Customers.Commands
{
    public record CreateInvoiceCommand : IRequest<InvoiceId>
    {
        public required InvoiceId Id { get; set; }
        public required CustomerId CustomerId { get; set; }
        public InvoiceNumber? Number { get; set; }
        public required Period PerformancePeriod { get; set; }
        public required Currency Currency { get; set; }
        public required decimal Tax { get; set; }
        public required string TaxDetails { get; set; }
        public OrderId? OrderId { get; set; }
        public OfferId? OfferId { get; set; }
    }
}
