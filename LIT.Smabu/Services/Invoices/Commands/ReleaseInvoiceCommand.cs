using LIT.Smabu.Domain.Shared.Invoices;
using LIT.Smabu.Shared.Invoices;
using MediatR;

namespace LIT.Smabu.Business.Service.Invoices.Commands
{
    public record ReleaseInvoiceCommand : IRequest<InvoiceDTO>
    {
        public required InvoiceId Id { get; set; }
        public InvoiceNumber? Number { get; set; }
        public DateTime ReleasedOn { get; set; }
    }
}
