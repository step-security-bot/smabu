using LIT.Smabu.Domain.Shared.Common;
using LIT.Smabu.Domain.Shared.Invoices;
using LIT.Smabu.Shared.Invoices;
using MediatR;

namespace LIT.Smabu.Business.Service.Invoices.Commands
{
    public record EditInvoiceCommand : IRequest<InvoiceDTO>
    {
        public required InvoiceId Id { get; set; }
        public required DatePeriod PerformancePeriod { get; set; }
        public required decimal Tax { get; set; }
        public required string TaxDetails { get; set; }
        public DateOnly? InvoiceDate { get; set; }
    }
}
