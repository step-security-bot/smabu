using LIT.Smabu.Shared.Entities.Business.CustomerAggregate;
using LIT.Smabu.Shared.Entities.Business.InvoiceAggregate;

namespace LIT.Smabu.Shared.Dtos
{
    public class InvoiceOverviewDto
    {
        public InvoiceOverviewDto(InvoiceId id, CustomerId customerId, CustomerNumber customerNumber, string customerName, InvoiceNumber number)
        {
            Id = id;
            CustomerId = customerId;
            CustomerNumber = customerNumber;
            CustomerName = customerName;
            Number = number;
        }

        public InvoiceId Id { get; }
        public CustomerId CustomerId { get; }
        public CustomerNumber CustomerNumber { get; }
        public string CustomerName { get; }
        public InvoiceNumber Number { get; }

        public static InvoiceOverviewDto From(Invoice invoice, Customer customer)
        {
            return new InvoiceOverviewDto(invoice.Id, invoice.CustomerId, customer.Number, customer.Name, invoice.Number);
        }
    }
}
