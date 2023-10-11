using LIT.Smabu.Business.Service.Invoices.Mappings;
using LIT.Smabu.Domain.Shared.Invoices;
using LIT.Smabu.Infrastructure.Shared.Contracts;
using LIT.Smabu.Shared.Invoices;
using MediatR;

namespace LIT.Smabu.Business.Service.Invoices.Commands
{
    public class PublishInvoiceHandler : IRequestHandler<PublishInvoiceCommand, InvoiceDTO>
    {
        private readonly IAggregateStore aggregateStore;

        public PublishInvoiceHandler(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

        public async Task<InvoiceDTO> Handle(PublishInvoiceCommand request, CancellationToken cancellationToken)
        {
            var invoice = await aggregateStore.GetByAsync(request.Id);
            var number = request.Number ?? await CreateNewNumberAsync(invoice.FiscalYear);
            invoice.Publish(number, request.InvoiceDate);
            await aggregateStore.AddOrUpdateAsync(invoice);
            return await new InvoiceMapper(this.aggregateStore).MapAsync(invoice);
        }

        private async Task<InvoiceNumber> CreateNewNumberAsync(int year)
        {
            var lastNumber = (await aggregateStore.GetAllAsync<Invoice>())
                .Select(x => x.Number)
                .OrderByDescending(x => x)
                .FirstOrDefault();

            return lastNumber == null ? InvoiceNumber.CreateFirst(year) : InvoiceNumber.CreateNext(lastNumber);
        }
    }
}
