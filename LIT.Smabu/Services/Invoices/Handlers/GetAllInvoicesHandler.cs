using LIT.Smabu.Infrastructure.CQRS;
using LIT.Smabu.Infrastructure.DDD;
using LIT.Smabu.Shared.Domain.Customers;
using LIT.Smabu.Shared.Domain.Customers.Queries;
using LIT.Smabu.Shared.Domain.Invoices;

namespace LIT.Smabu.Business.Service.Invoices.Handlers
{
    public class GetAllInvoicesHandler : RequestHandler<GetAllInvoicesQuery, GetAllInvoicesResponse[]>
    {
        public GetAllInvoicesHandler(IAggregateStore aggregateStore) : base(aggregateStore)
        {
        }

        public override Task<GetAllInvoicesResponse[]> Handle(GetAllInvoicesQuery request, CancellationToken cancellationToken)
        {
            var invoices = AggregateStore.GetAll<Invoice>();
            var customerIds = invoices.Select(x => x.CustomerId).Distinct().ToList();
            var customers = AggregateStore.GetByIds<Customer, CustomerId>(customerIds);
            var result = invoices
                .Select(x => GetAllInvoicesResponse.Map(x, customers.Single(y => y.Id == x.CustomerId)))
                .OrderByDescending(x => x.Number)
                .ToArray();
            return Task.FromResult(result);
        }
    }
}
