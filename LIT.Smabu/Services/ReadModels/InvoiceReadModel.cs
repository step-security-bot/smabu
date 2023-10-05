using LIT.Smabu.Infrastructure.CQRS;
using LIT.Smabu.Infrastructure.DDD;
using LIT.Smabu.Shared.Domain.Customers;
using LIT.Smabu.Shared.Domain.Invoices;
using LIT.Smabu.Shared.Invoices;

namespace LIT.Smabu.Business.Service.ReadModels
{
    public class InvoiceReadModel : EntityReadModel<Invoice, InvoiceId>
    {
        public InvoiceReadModel(IAggregateStore aggregateStore) : base(aggregateStore)
        {

        }

        protected override IEnumerable<Invoice> BuildQuery(IAggregateStore aggregateStore)
        {
            return aggregateStore.GetAll<Invoice>().OrderByDescending(x => x.Number);
        }

        public List<InvoiceOverviewDto> GetOverview()
        {
            var invoices = GetAll();
            var customerIds = invoices.Select(x => x.CustomerId).Distinct().ToList();
            var customers = AggregateStore.GetByIds<Customer, CustomerId>(customerIds);
            var result = invoices.Select(x => InvoiceOverviewDto.From(x, customers.Single(y => y.Id == x.CustomerId))).ToList();
            return result;
        }
    }
}
