using LIT.Smabu.Shared.Entities.Business.CustomerAggregate;

namespace LIT.Smabu.Shared.Dtos
{
    public class CustomerDetailDto
    {
        public CustomerDetailDto(CustomerId id, string name1, string name2)
        {
            Id = id;
            Name1 = name1;
            Name2 = name2;
        }

        public CustomerId Id { get; set; }
        public string Name1 { get; set; }
        public string Name2 { get; set; }

        public static CustomerDetailDto From(Customer customer)
        {
            return new CustomerDetailDto(customer.Id, customer.Name1, customer.Name2);
        }
    }
}
