using LIT.Smabu.Business.Service.Mapping;
using LIT.Smabu.Domain.Shared.Invoices;
using LIT.Smabu.Infrastructure.Shared.Contracts;
using LIT.Smabu.Shared.Invoices;
using MediatR;

namespace LIT.Smabu.Business.Service.Invoices.Queries
{
    public class GetInvoicesHandler : IRequestHandler<GetInvoicesQuery, InvoiceDTO[]>
    {
        private readonly IAggregateStore aggregateStore;
        private readonly IMapper mapper;

        public GetInvoicesHandler(IAggregateStore aggregateStore, IMapper mapper)
        {
            this.aggregateStore = aggregateStore;
            this.mapper = mapper;
        }

        public async Task<InvoiceDTO[]> Handle(GetInvoicesQuery request, CancellationToken cancellationToken)
        {
            var invoices = await aggregateStore.GetAsync<Invoice>();
            var result = this.mapper.Map<Invoice, InvoiceDTO>(invoices)
                .OrderByDescending(x => x.Number)
                .ToArray();
            return result;
        }
    }
}
