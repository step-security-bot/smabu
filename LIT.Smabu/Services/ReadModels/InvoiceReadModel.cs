using LIT.Smabu.Infrastructure.CQRS;
using LIT.Smabu.Infrastructure.DDD;
using LIT.Smabu.Shared.Entities.Business.InvoiceAggregate;

namespace LIT.Smabu.Service.ReadModels
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
