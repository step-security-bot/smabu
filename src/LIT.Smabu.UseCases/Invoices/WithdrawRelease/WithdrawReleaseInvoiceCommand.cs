using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.UseCases.Invoices;
using LIT.Smabu.UseCases.SeedWork;

namespace LIT.Smabu.UseCases.Invoices.WithdrawRelease
{
    public record WithdrawReleaseInvoiceCommand : ICommand<InvoiceDTO>
    {
        public required InvoiceId Id { get; set; }
    }
}
