using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.Shared.Interfaces;
using LIT.Smabu.UseCases.Invoices;

namespace LIT.Smabu.UseCases.Invoices.WithdrawRelease
{
    public record WithdrawReleaseInvoiceCommand : ICommand<InvoiceDTO>
    {
        public required InvoiceId Id { get; set; }
    }
}
