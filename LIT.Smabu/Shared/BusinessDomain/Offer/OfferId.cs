﻿using LIT.Smabu.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIT.Smabu.Shared.BusinessDomain.Offer
{
    public class OfferId : EntityId<Offer>
    {
        public OfferId(Guid value) : base(value)
        {
        }
    }
}
