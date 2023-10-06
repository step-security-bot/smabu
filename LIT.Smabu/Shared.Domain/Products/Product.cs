using LIT.Smabu.Domain.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIT.Smabu.Domain.Shared.Products
{
    public class Product : Entity<ProductId>
    {
        public override ProductId Id => throw new NotImplementedException();
    }
}
