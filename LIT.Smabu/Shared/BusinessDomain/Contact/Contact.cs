using LIT.Smabu.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIT.Smabu.Shared.BusinessDomain.Contact
{
    public class Contact : Entity<ContactId>
    {
        public override ContactId Id => throw new NotImplementedException();
    }
}
