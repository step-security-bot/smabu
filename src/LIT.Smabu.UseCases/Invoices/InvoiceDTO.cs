using LIT.Smabu.Domain.Common;
using LIT.Smabu.Domain.CustomerAggregate;
using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.Shared.Interfaces;
using LIT.Smabu.UseCases.Customers;

namespace LIT.Smabu.UseCases.Invoices
{
    public record InvoiceDTO : IDTO
    {
        public string DisplayName => $"{Number?.Long} {Customer?.ShortName}";
        public InvoiceId Id { get; set; }
        public DateTime? CreatedOn { get; set; }
        public CustomerDTO Customer { get; set; }
        public InvoiceNumber Number { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public virtual DatePeriod PerformancePeriod { get; set; }
        public int FiscalYear { get; set; }
        public decimal Tax { get; set; }
        public string TaxDetails { get; set; }
        public bool IsReleased { get; set; }
        public DateTime? ReleasedOn { get; set; }

        public static InvoiceDTO From(Invoice invoice, Customer customer)
        {
            return new()
            {
                Id = invoice.Id,
                Customer = CustomerDTO.CreateFrom(customer),
                Number = invoice.Number,
                Amount = invoice.Amount,
                Currency = invoice.Currency,
                PerformancePeriod = invoice.PerformancePeriod,
                FiscalYear = invoice.FiscalYear,
                Tax = invoice.Tax,
                TaxDetails = invoice.TaxDetails,
                IsReleased = invoice.IsReleased,
                ReleasedOn = invoice.ReleasedOn,
                CreatedOn = invoice.Meta?.CreatedOn
            };
        }
    }
}