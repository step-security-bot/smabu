using LIT.Smabu.Domain.Shared.Common;
using LIT.Smabu.Domain.Shared.Customers;
using LIT.Smabu.Domain.Shared.Invoices;
using LIT.Smabu.Domain.Shared.Offers;
using LIT.Smabu.Domain.Shared.Orders;
using MediatR;

namespace LIT.Smabu.Business.Service.Invoices.Commands
{
    public record CreateInvoiceCommand : IRequest<InvoiceId>
    {
        public required InvoiceId Id { get; set; }
        public required CustomerId CustomerId { get; set; }
        public InvoiceNumber? Number { get; set; }
        public required DatePeriod PerformancePeriod { get; set; }
        public required Currency Currency { get; set; }
        public required decimal Tax { get; set; }
        public required string TaxDetails { get; set; }
        public OrderId? OrderId { get; set; }
        public OfferId? OfferId { get; set; }
    }
}
