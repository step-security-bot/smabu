using LIT.Smabu.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIT.Smabu.Shared.BusinessDomain.Product
{
    public class Product : Entity<ProductId>
    {
        public override ProductId Id => throw new NotImplementedException();
    }
}
