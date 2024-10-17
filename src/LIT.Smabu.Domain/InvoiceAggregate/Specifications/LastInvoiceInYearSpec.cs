using LIT.Smabu.Shared;

namespace LIT.Smabu.Domain.InvoiceAggregate.Specifications
{
    public class LastInvoiceInYearSpec : Specification<Invoice>
    {
        public LastInvoiceInYearSpec(int fiscalYear) : base(x => !x.Number.IsTemporary && x.Number.Value.ToString().StartsWith(fiscalYear.ToString()))
        {
            OrderByDescendingExpression = x => x.Number.Long;
            Take = 1;
        }
    }
}
