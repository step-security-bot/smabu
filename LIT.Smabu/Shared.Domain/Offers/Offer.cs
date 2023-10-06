using LIT.Smabu.Domain.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIT.Smabu.Domain.Shared.Offers
{
    public class Offer : Entity<OfferId>
    {
        public override OfferId Id => throw new NotImplementedException();
    }
}
