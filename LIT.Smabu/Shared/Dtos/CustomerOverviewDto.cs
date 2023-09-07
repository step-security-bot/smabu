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
        public CustomerOverviewDto(CustomerId id, CustomerNumber number, string name1, string name2, string industryBranch)
        {
            Id = id;
            Number = number;
            Name1 = name1;
            Name2 = name2;
            IndustryBranch = industryBranch;
        }

        public CustomerId Id { get; }
        public CustomerNumber Number { get; }
        public string Name1 { get; }
        public string Name2 { get; }
        public string IndustryBranch { get; }

        public static CustomerOverviewDto From(Customer customer)
        {
            return new CustomerOverviewDto(customer.Id, customer.Number, customer.Name1, customer.Name2, customer.IndustryBranch);
        }
    }
}
