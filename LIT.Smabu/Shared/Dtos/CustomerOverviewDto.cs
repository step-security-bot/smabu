using LIT.Smabu.Shared.Entities.Business.CustomerAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIT.Smabu.Shared.Dtos
{
    public class CustomerOverviewDto
    {
        public CustomerOverviewDto(CustomerId id)
        {
            Id = id;
        }

        public CustomerId Id { get; }

        public static CustomerOverviewDto From(Customer customer)
        {
            return new CustomerOverviewDto(customer.Id);
        }
    }
}
