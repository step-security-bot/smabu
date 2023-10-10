using LIT.Smabu.Domain.Shared.Common;
using LIT.Smabu.Domain.Shared.Invoices;
using LIT.Smabu.Shared.Contracts;
using LIT.Smabu.Shared.Customers;

namespace LIT.Smabu.Shared.Invoices
{
    public class InvoiceDTO : IDTO
    {
        public InvoiceId Id { get; set; }
        public CustomerDTO Customer { get; set; }
        public InvoiceNumber Number { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public DatePeriod PerformancePeriod { get; set; }
        public int FiscalYear { get; set; }
        public decimal Tax { get; set; }
        public string TaxDetails { get; set; }
        public List<InvoiceLineDTO> InvoiceLines { get; set; }
    }
}