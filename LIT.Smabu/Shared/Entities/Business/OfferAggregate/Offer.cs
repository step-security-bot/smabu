using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIT.Smabu.Shared.Entities.Business.OfferAggregate
{
    public class Offer : Entity<OfferId>
    {
        public override OfferId Id => throw new NotImplementedException();
    }
}
