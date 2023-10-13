using LIT.Smabu.Domain.Shared.Common;
using LIT.Smabu.Domain.Shared.Invoices;

namespace LIT.Smabu.Shared.Invoices
{
    public class AddInvoiceItemDTO
    {
        public required InvoiceItemId Id { get; set; }
        public required InvoiceId InvoiceId { get; set; }
        public string Details { get; set; } = string.Empty;
        public Quantity Quantity { get; set; } = Quantity.Empty();
        public decimal UnitPrice { get; set; } = 0;
    }
}
