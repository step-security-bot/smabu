using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIT.Smabu.Shared.Entities.Business.ContactAggregate
{
    public class Contact : Entity<ContactId>
    {
        public override ContactId Id => throw new NotImplementedException();
    }
}
