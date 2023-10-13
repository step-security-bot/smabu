using LIT.Smabu.Domain.Shared.Common;
using LIT.Smabu.Domain.Shared.Customers;
using LIT.Smabu.Domain.Shared.Invoices;
using LIT.Smabu.Domain.Shared.Offers;
using LIT.Smabu.Domain.Shared.Orders;
using LIT.Smabu.Shared.Invoices;
using MediatR;

namespace LIT.Smabu.Business.Service.Invoices.Commands
{
    public record CreateInvoiceCommand : IRequest<InvoiceDTO>
    {
        public required InvoiceId Id { get; set; }
        public required CustomerId CustomerId { get; set; }
        public required int FiscalYear { get; set; }
        public required Currency Currency { get; set; }
        public DatePeriod? PerformancePeriod { get; set; }
        public decimal Tax { get; set; }
        public string TaxDetails { get; set; }
        public OrderId? OrderId { get; set; }
        public OfferId? OfferId { get; set; }
    }
}
