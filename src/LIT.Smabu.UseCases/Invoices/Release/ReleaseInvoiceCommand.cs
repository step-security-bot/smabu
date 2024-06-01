using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.UseCases.SeedWork;

namespace LIT.Smabu.UseCases.Invoices.Release
{
    public record ReleaseInvoiceCommand : ICommand<InvoiceDTO>
    {
        public required InvoiceId Id { get; set; }
        public InvoiceNumber? Number { get; set; }
        public DateTime ReleasedOn { get; set; }
    }
}
