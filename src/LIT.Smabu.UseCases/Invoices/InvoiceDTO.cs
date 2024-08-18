using LIT.Smabu.Domain.Common;
using LIT.Smabu.Domain.CustomerAggregate;
using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.UseCases.Customers;
using LIT.Smabu.UseCases.SeedWork;

namespace LIT.Smabu.UseCases.Invoices
{
    public record InvoiceDTO : IDTO
    {
        public InvoiceDTO(InvoiceId id, DateTime? createdOn, CustomerDTO customer, InvoiceNumber number, decimal amount,
                          Currency currency, DatePeriod performancePeriod, int fiscalYear, decimal tax,
                          string taxDetails, bool isReleased, DateTime? releasedOn)
        {
            Id = id;
            CreatedOn = createdOn;
            Customer = customer;
            Number = number;
            Amount = amount;
            Currency = currency;
            PerformancePeriod = performancePeriod;
            FiscalYear = fiscalYear;
            Tax = tax;
            TaxDetails = taxDetails;
            IsReleased = isReleased;
            ReleasedOn = releasedOn;
        }

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
        public List<InvoiceItemDTO>? Items { get; set; }

        public static InvoiceDTO From(Invoice invoice, Customer customer, bool withItems = false)
        {
            var result = new InvoiceDTO(invoice.Id, invoice.Meta?.CreatedOn, CustomerDTO.CreateFrom(customer), invoice.Number,
                invoice.Amount, invoice.Currency, invoice.PerformancePeriod, invoice.FiscalYear, invoice.Tax,
                invoice.TaxDetails, invoice.IsReleased, invoice.ReleasedOn);

            if (withItems)
            {
                result.Items = invoice.Items.Select(InvoiceItemDTO.From).ToList();
            }

            return result;
        }
    }
}