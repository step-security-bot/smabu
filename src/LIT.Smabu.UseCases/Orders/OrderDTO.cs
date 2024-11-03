using LIT.Smabu.Domain.CustomerAggregate;
using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.Domain.OfferAggregate;
using LIT.Smabu.Domain.OrderAggregate;
using LIT.Smabu.UseCases.Customers;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Orders
{
    public partial record OrderDTO : IDTO
    {
        public OrderDTO(OrderId id, OrderNumber number, DateTime createdAt, CustomerDTO customer, string name, string description,
            DateOnly orderDate, DateTime? deadline, string bunchKey, OrderReferencesDTO references)
        {
            Id = id;
            Number = number;
            CreatedAt = createdAt;
            Customer = customer;
            Name = name;
            Description = description;
            OrderDate = orderDate;
            Deadline = deadline;
            BunchKey = bunchKey;
            References = references;
        }

        public string DisplayName => Number.Long + "/" + Customer.CorporateDesign.ShortName + "/" + OrderDate.ToShortDateString();
        public OrderId Id { get; set; }
        public OrderNumber Number { get; }
        public DateTime CreatedAt { get; set; }
        public CustomerDTO Customer { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateOnly OrderDate { get; set; }
        public DateTime? Deadline { get; set; }
        public string BunchKey { get; set; }
        public OrderReferencesDTO References { get; set; }

        public static OrderDTO Create(Order order, Customer customer, OrderReferencesDTO orderReferences)
        {
            var customerDto = CustomerDTO.Create(customer);
            var result = new OrderDTO(order.Id, order.Number, order.Meta!.CreatedAt, customerDto, order.Name, order.Description,
                order.OrderDate, order.Deadline, order.BunchKey, orderReferences);

            return result;
        }
        public partial record OrderReferencesDTO(List<OrderReferenceDTO<OfferId>> Offers, List<OrderReferenceDTO<InvoiceId>> Invoices)
        {
            internal static OrderReferencesDTO Create(OrderReferences references, List<Offer> allOffers, List<Invoice> allInvoices)
            {
                var offerReferences = references.OfferIds.Select(oId => allOffers.Single(offer => offer.Id == oId))
                    .Select(offer => new OrderReferenceDTO<OfferId>(offer.Id, offer.Number.ToString(), true, offer.OfferDate, offer.Amount))
                    .ToList();

                var invoiceReferences = references.InvoiceIds.Select(iId => allInvoices.Single(invoice => invoice.Id == iId))
                    .Select(invoice => new OrderReferenceDTO<InvoiceId>(invoice.Id, invoice.Number.ToString(), true, invoice.InvoiceDate, invoice.Amount))
                    .ToList();

                return new OrderReferencesDTO(offerReferences, invoiceReferences);
            }
        }
    }
}