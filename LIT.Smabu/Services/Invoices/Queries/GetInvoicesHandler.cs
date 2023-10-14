using LIT.Smabu.Business.Service.Invoices.Mappers;
using LIT.Smabu.Domain.Shared.Invoices;
using LIT.Smabu.Infrastructure.Shared.Contracts;
using LIT.Smabu.Shared.Invoices;
using MediatR;

namespace LIT.Smabu.Business.Service.Invoices.Queries
{
    public class GetInvoicesHandler : IRequestHandler<GetInvoicesQuery, InvoiceDTO[]>
    {
        private readonly IAggregateStore aggregateStore;

        public GetInvoicesHandler(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

        public async Task<InvoiceDTO[]> Handle(GetInvoicesQuery request, CancellationToken cancellationToken)
        {
            var invoices = await aggregateStore.GetAllAsync<Invoice>();
            var result = (await new InvoiceMapper(this.aggregateStore).MapAsync(invoices)).Values
                .OrderByDescending(x => x.Number)
                .ToArray();
            return result;
        }
    }
}
