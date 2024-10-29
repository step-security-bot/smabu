using LIT.Smabu.Domain.CustomerAggregate;
using LIT.Smabu.Domain.Shared;

namespace LIT.Smabu.Domain.OrderAggregate
{
    public class Order(OrderId id, OrderNumber number, CustomerId customerId, string name, string description, DateOnly orderDate, OrderStatus status, DateTime? deadline, string orderGroup = "") : AggregateRoot<OrderId>
    {
        public override OrderId Id { get; } = id;
        public OrderNumber Number { get; private set; } = number;
        public CustomerId CustomerId { get; } = customerId;
        public string Name { get; private set; } = name;
        public string Description { get; private set; } = description;
        public DateOnly OrderDate { get; private set; } = orderDate;
        public DateTime? Deadline { get; private set; } = deadline;
        public string OrderGroup { get; private set; } = orderGroup;
        public OrderStatus Status { get; private set; } = status;

        public OrderReferences References = OrderReferences.Empty;

        public static Order Create(OrderId id, OrderNumber number, CustomerId customerId, string name, DateOnly orderDate)
        {
            if (customerId == null || number == null || id == null || string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("customerId, number, id, and name cannot be empty.");
            }
            return new Order(id, number, customerId, name, "", orderDate, OrderStatus.InProgress, null);
        }

        public Result Update(string name, string description, DateOnly orderDate, string orderGroup, DateTime? deadline, OrderStatus status)
        {
            Name = name;
            OrderDate = orderDate;
            Description = description;
            Status = status;
            OrderGroup = orderGroup;
            Deadline = deadline;

            return Result.Success();
        }

        public Result UpdateReferences(OrderReferences references)
        {
            References = references;
            return Result.Success();
        }

        public override Result Delete()
        {
            return base.Delete();
        }
    }
}

