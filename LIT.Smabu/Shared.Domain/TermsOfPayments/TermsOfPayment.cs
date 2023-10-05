using LIT.Smabu.Shared.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIT.Smabu.Shared.Domain.TermsOfPayments
{
    public class TermsOfPayment : Entity<TermsOfPaymentId>
    {
        public override TermsOfPaymentId Id => throw new NotImplementedException();
    }
}
