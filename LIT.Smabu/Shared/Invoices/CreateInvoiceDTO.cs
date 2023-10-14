using LIT.Smabu.Domain.Shared.Common;
using LIT.Smabu.Domain.Shared.Customers;
using LIT.Smabu.Domain.Shared.Invoices;
using LIT.Smabu.Shared.Contracts;

namespace LIT.Smabu.Shared.Invoices
{
    public record CreateInvoiceDTO : IDTO
    {
        public InvoiceId Id { get; set; }
        public CustomerId? CustomerId { get; set; }
        public int FiscalYear { get; set; } = DateTime.Now.Year;
        public int[] FiscalYears => new int[] { DateTime.Now.Year + 1, DateTime.Now.Year + 0, DateTime.Now.Year + 1 };
        public Currency Currency { get; set; }
    }
}
