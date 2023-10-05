using LIT.Smabu.Shared.Domain.Common;
using LIT.Smabu.Shared.Domain.Customers;
using LIT.Smabu.Shared.Domain.Invoices;

namespace LIT.Smabu.Shared.Invoices
{
    public class InvoiceOverviewDto
    {
        public InvoiceOverviewDto(InvoiceId id, CustomerId customerId, CustomerNumber customerNumber, string customerName, InvoiceNumber number,
            decimal amount, Currency currency, Period performance, int fiscalYear)
        {
            Id = id;
            CustomerId = customerId;
            CustomerNumber = customerNumber;
            CustomerName = customerName;
            Number = number;
            Amount = amount;
            Currency = currency;
            Performance = performance;
            FiscalYear = fiscalYear;
        }

        public InvoiceId Id { get; }
        public CustomerId CustomerId { get; }
        public CustomerNumber CustomerNumber { get; }
        public string CustomerName { get; }
        public InvoiceNumber Number { get; }
        public decimal Amount { get; }
        public Currency Currency { get; }
        public Period Performance { get; }
        public int FiscalYear { get; }

        public static InvoiceOverviewDto From(Invoice invoice, Customer customer)
        {
            return new InvoiceOverviewDto(invoice.Id, invoice.CustomerId, customer.Number, customer.Name, invoice.Number,
                invoice.Amount, invoice.Currency, invoice.PerformancePeriod, invoice.FiscalYear);
        }
    }
}
