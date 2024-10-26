using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.Domain.OfferAggregate;
using LIT.Smabu.Domain.OrderAggregate;
using LIT.Smabu.UseCases.Customers;
using LIT.Smabu.UseCases.Shared;
using Customer = LIT.Smabu.Domain.CustomerAggregate.Customer;

namespace LIT.Smabu.UseCases.Orders
{
    public record OrderDTO : IDTO
    {
        public OrderDTO(OrderId id, OrderNumber number, DateTime createdOn, CustomerDTO customer, string name, string description,
            DateOnly orderDate, DateTime? deadline, string orderGroup, OrderStatus status, 
            List<OrderReferenceItem<OfferId>>? offers = null,
            List<OrderReferenceItem<InvoiceId>>? invoices = null)
        {
            Id = id;
            Number = number;
            CreatedOn = createdOn;
            Customer = customer;
            Name = name;
            Description = description;
            OrderDate = orderDate;
            Deadline = deadline;
            OrderGroup = orderGroup;
            Status = status;
            Offers = offers;
            Invoices = invoices;
        }

        public string DisplayName => Number.Long + "/" + Customer.ShortName + "/" + OrderDate.ToShortDateString();
        public OrderId Id { get; set; }
        public OrderNumber Number { get; }
        public DateTime CreatedOn { get; set; }
        public CustomerDTO Customer { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateOnly OrderDate { get; set; }
        public DateTime? Deadline { get; set; }
        public string OrderGroup { get; set; }
        public OrderStatus Status { get; set; }

        public List<OrderReferenceItem<OfferId>>? Offers { get; set; }
        public List<OrderReferenceItem<InvoiceId>>? Invoices { get; set; }


        public static OrderDTO Create(Order order, Customer customer,
            List<OrderReferenceItem<OfferId>>? offers = null, 
            List<OrderReferenceItem<InvoiceId>>? invoices = null)
        {
            var customerDto = CustomerDTO.Create(customer);
            var result = new OrderDTO(order.Id, order.Number, order.Meta!.CreatedOn, customerDto, order.Name, order.Description,
                order.OrderDate, order.Deadline, order.OrderGroup, order.Status, offers, invoices);

            return result;
        }
    }
}