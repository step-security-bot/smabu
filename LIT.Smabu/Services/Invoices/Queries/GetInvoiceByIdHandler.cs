using LIT.Smabu.Business.Service.Invoices.Mappers;
using LIT.Smabu.Infrastructure.Shared.Contracts;
using LIT.Smabu.Shared.Invoices;
using MediatR;

namespace LIT.Smabu.Business.Service.Invoices.Queries
{
    public class GetInvoiceByIdHandler : IRequestHandler<GetInvoiceByIdQuery, InvoiceDTO>
    {
        private readonly IAggregateStore aggregateStore;

        public GetInvoiceByIdHandler(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

        public async Task<InvoiceDTO> Handle(GetInvoiceByIdQuery request, CancellationToken cancellationToken)
        {
            var invoices = await aggregateStore.GetByAsync(request.Id);
            var result = await new InvoiceMapper(this.aggregateStore).MapAsync(invoices);
            return result;
        }
    }
}
