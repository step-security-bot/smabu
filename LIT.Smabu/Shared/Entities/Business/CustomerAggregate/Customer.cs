using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIT.Smabu.Shared.Entities.Business.CustomerAggregate
{
    public class Customer : AggregateRoot<CustomerId>
    {
        public Customer(CustomerId id, CustomerNumber number, string name1, string name2, string industryBranch)
        {
            Id = id;
            Number = number;
            Name1 = name1;
            Name2 = name2;
            IndustryBranch = industryBranch;
        }

        public override CustomerId Id { get; }
        public CustomerNumber Number { get; private set; }
        public string Name1 { get; private set; }
        public string Name2 { get; private set; }
        public string IndustryBranch { get; private set; }

        public static Customer Create(CustomerId id, CustomerNumber number, string name1, string name2, string industryBranch)
        {
            return new Customer(id, number, name1, name2, industryBranch);
        }
    }
}
