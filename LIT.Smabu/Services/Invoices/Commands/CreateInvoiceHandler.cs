using LIT.Smabu.Domain.Shared.Invoices;
using LIT.Smabu.Infrastructure.Shared.Contracts;
using MediatR;

namespace LIT.Smabu.Business.Service.Invoices.Commands
{
    public class CreateInvoiceHandler : IRequestHandler<CreateInvoiceCommand, InvoiceId>
    {
        private readonly IAggregateStore aggregateStore;

        public CreateInvoiceHandler(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

        public async Task<InvoiceId> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            request.Number ??= await CreateNewNumberAsync(request.PerformancePeriod.To.Year);
            var invoice = Invoice.Create(request.Id, request.CustomerId, request.Number, request.PerformancePeriod,
                request.Currency, request.Tax, request.TaxDetails, request.OrderId, request.OfferId);
            await aggregateStore.AddOrUpdateAsync(invoice);
            return invoice.Id;
        }

        private async Task<InvoiceNumber> CreateNewNumberAsync(int year)
        {
            var lastNumber = (await aggregateStore.GetAsync<Invoice>())
                .Select(x => x.Number)
                .OrderByDescending(x => x)
                .FirstOrDefault();

            return lastNumber == null ? InvoiceNumber.CreateFirst(year) : InvoiceNumber.CreateNext(lastNumber);
        }
    }
}
