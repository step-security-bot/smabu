using LIT.Smabu.Infrastructure.CQRS;
using LIT.Smabu.Infrastructure.DDD;
using LIT.Smabu.Shared.BusinessDomain.Invoice;

namespace LIT.Smabu.Server.ReadModels
{
    public class InvoiceReadModel : EntityReadModel<Invoice, InvoiceId>
    {
        public InvoiceReadModel(IAggregateStore aggregateStore) : base(aggregateStore)
        {

        }

        protected override IEnumerable<Invoice> BuildQuery(IAggregateStore aggregateStore)
        {
            return aggregateStore.GetAll<Invoice, InvoiceId>();
        }
    }
}
