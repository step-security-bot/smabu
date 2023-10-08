using LIT.Smabu.Domain.Shared.Invoices;
using LIT.Smabu.Shared.Invoices;
using MediatR;

namespace LIT.Smabu.Business.Service.Invoices.Queries
{
    public record GetInvoiceByIdQuery : IRequest<InvoiceDTO>
    {
        public GetInvoiceByIdQuery(InvoiceId id)
        {
            Id = id;
        }

        public  InvoiceId Id { get; }
    }
}