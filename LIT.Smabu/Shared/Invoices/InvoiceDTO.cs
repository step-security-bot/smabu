using LIT.Smabu.Domain.Shared.Common;
using LIT.Smabu.Domain.Shared.Customers;
using LIT.Smabu.Domain.Shared.Invoices;

namespace LIT.Smabu.Shared.Invoices
{
    public class InvoiceDTO
    {
        public InvoiceId Id { get; set; }
        public CustomerId CustomerId { get; set; }
        public CustomerNumber CustomerNumber { get; set; }
        public string CustomerName { get; set; }
        public InvoiceNumber Number { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public DatePeriod PerformancePeriod { get; set; }
        public int FiscalYear { get; set; }

        public static InvoiceDTO Map(Invoice invoice, Customer customer)
        {
            return new InvoiceDTO
            {
                Id = invoice.Id,
                CustomerId = customer.Id,
                CustomerNumber = customer.Number,
                CustomerName = customer.Name,
                Number = invoice.Number,
                Currency = invoice.Currency,
                PerformancePeriod = invoice.PerformancePeriod,
                FiscalYear = invoice.FiscalYear,
                Amount = invoice.InvoiceLines.Sum(x => x.TotalPrice)
            };
        }
    }
}