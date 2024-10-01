using LIT.Smabu.Domain.Common;
using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.UseCases.SeedWork;

namespace LIT.Smabu.UseCases.Invoices.Update
{
    public record UpdateInvoiceCommand : ICommand<InvoiceId>
    {
        public required InvoiceId Id { get; set; }
        public required DatePeriod PerformancePeriod { get; set; }
        public required TaxRate TaxRate { get; set; }
        public DateOnly? InvoiceDate { get; set; }
    }
}
