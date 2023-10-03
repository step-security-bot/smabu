using LIT.Smabu.Infrastructure.CQRS;
using LIT.Smabu.Infrastructure.DDD;
using LIT.Smabu.Shared.Dtos;
using LIT.Smabu.Shared.Entities.Business.CustomerAggregate;
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
            return aggregateStore.GetAll<Invoice, InvoiceId>().OrderByDescending(x => x.Number);
        }

        public List<InvoiceOverviewDto> GetOverview()
        {
            var invoices = this.GetAll();
            var customerIds = invoices.Select(x => x.CustomerId).Distinct().ToList();
            var customers = this.AggregateStore.GetByIds<Customer, CustomerId>(customerIds);
            var result = invoices.Select(x => InvoiceOverviewDto.From(x, customers.Single(y => y.Id == x.CustomerId))).ToList();
            return result;
        }

        //public InvoiceOverviewDto GetDetail(InvoiceId id)
        //{
        //    InvoiceOverviewDto.From(GetById(id) ?? throw new EntityNotFoundException(id));
        //}
    }
}
