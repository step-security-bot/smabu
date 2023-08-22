using LIT.Smabu.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIT.Smabu.Shared.BusinessDomain.Customer
{
    public class Customer : Entity<CustomerId>
    {
        public override CustomerId Id => throw new NotImplementedException();
    }
}
