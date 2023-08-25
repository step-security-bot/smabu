using LIT.Smabu.Shared.Entities.Business.CustomerAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIT.Smabu.Shared.Dtos
{
    public class CustomerDetailDto
    {
        public CustomerDetailDto(CustomerId id)
        {
            Id = id;
        }

        public CustomerId Id { get; }

        public static CustomerDetailDto From(Customer customer)
        {
            return new CustomerDetailDto(customer.Id);
        }
    }
}
