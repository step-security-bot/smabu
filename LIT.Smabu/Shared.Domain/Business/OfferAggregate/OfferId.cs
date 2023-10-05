using LIT.Smabu.Shared.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIT.Smabu.Shared.Domain.Business.OfferAggregate
{
    public class OfferId : EntityId<Offer>
    {
        public OfferId(Guid value) : base(value)
        {
        }
    }
}
