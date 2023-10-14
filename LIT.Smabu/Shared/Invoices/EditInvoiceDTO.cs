using LIT.Smabu.Domain.Shared.Common;
using LIT.Smabu.Domain.Shared.Invoices;
using LIT.Smabu.Shared.Contracts;

namespace LIT.Smabu.Shared.Invoices
{
    public class EditInvoiceDTO : IDTO
    {
        public InvoiceId Id { get; set; }
        public DatePeriod PerformancePeriod { get; set; }
        public decimal Tax { get; set; }
        public string TaxDetails { get; set; }
    }
}
