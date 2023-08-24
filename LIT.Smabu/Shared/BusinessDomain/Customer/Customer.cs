using LIT.Smabu.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIT.Smabu.Shared.BusinessDomain.Customer
{
    public class Customer : AggregateRoot<CustomerId>
    {
        public Customer(CustomerId id, string name1, string name2, string industryBranch)
        {
            Id = id;
            Name1 = name1;
            Name2 = name2;
            IndustryBranch = industryBranch;
        }

        public override CustomerId Id { get; }
        public string Name1 { get; private set; }
        public string Name2 { get; private set; }
        public string IndustryBranch { get; private set; }

        public static Customer Create(CustomerId id, string name1, string name2, string industryBranch)
        {
            return new Customer(id, name1, name2, industryBranch); 
        }
    }
}
