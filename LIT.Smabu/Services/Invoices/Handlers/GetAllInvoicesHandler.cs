using LIT.Smabu.Domain.Shared.Customers;
using LIT.Smabu.Domain.Shared.Invoices;
using LIT.Smabu.Domain.Shared.Invoices.Queries;
using LIT.Smabu.Infrastructure.CQRS;
using LIT.Smabu.Infrastructure.DDD;

namespace LIT.Smabu.Business.Service.Invoices.Handlers
{
    public class GetAllInvoicesHandler : RequestHandler<GetAllInvoicesQuery, GetAllInvoicesResponse[]>
    {
        public GetAllInvoicesHandler(IAggregateStore aggregateStore) : base(aggregateStore)
        {
        }

        public override async Task<GetAllInvoicesResponse[]> Handle(GetAllInvoicesQuery request, CancellationToken cancellationToken)
        {
            var invoices = await AggregateStore.GetAllAsync<Invoice>();
            GetAllInvoicesResponse[] result = await MapResultAsync(invoices);
            return result;
        }

        private async Task<GetAllInvoicesResponse[]> MapResultAsync(List<Invoice> invoices)
        {
            var customerIds = invoices.Select(x => x.CustomerId).Distinct().ToList();
            var customers = await AggregateStore.GetByIdsAsync<Customer, CustomerId>(customerIds);
            var result = invoices
                .Select(x => GetAllInvoicesResponse.Map(x, customers[x.CustomerId]))
                .OrderByDescending(x => x.Number)
                .ToArray();
            return result;
        }
    }
}
