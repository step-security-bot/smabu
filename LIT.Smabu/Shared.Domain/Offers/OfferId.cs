using LIT.Smabu.Domain.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIT.Smabu.Domain.Shared.Offers
{
    public class OfferId : EntityId<Offer>
    {
        public OfferId(Guid value) : base(value)
        {
        }
    }
}
