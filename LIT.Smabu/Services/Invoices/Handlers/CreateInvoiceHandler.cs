using LIT.Smabu.Domain.Shared.Invoices;
using LIT.Smabu.Domain.Shared.Invoices.Commands;
using LIT.Smabu.Infrastructure.CQRS;
using LIT.Smabu.Infrastructure.DDD;

namespace LIT.Smabu.Business.Service.Customers.Queries
{
    public class CreateInvoiceHandler : RequestHandler<CreateInvoiceCommand, InvoiceId>
    {
        public CreateInvoiceHandler(IAggregateStore aggregateStore) : base(aggregateStore)
        {

        }

        public override async Task<InvoiceId> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            request.Number ??= await CreateNewNumberAsync(request.PerformancePeriod.To.Year);
            var invoice = Invoice.Create(request.Id, request.CustomerId, request.Number, request.PerformancePeriod,
                request.Currency, request.Tax, request.TaxDetails, request.OrderId, request.OfferId);
            await this.AggregateStore.AddOrUpdateAsync(invoice);
            return invoice.Id;
        }

        private async Task<InvoiceNumber> CreateNewNumberAsync(int year)
        {
            var lastNumber = (await this.AggregateStore.GetAllAsync<Invoice>())
                .Select(x => x.Number)
                .OrderByDescending(x => x)
                .FirstOrDefault();

            return lastNumber == null ? InvoiceNumber.CreateFirst(year) : InvoiceNumber.CreateNext(lastNumber);
        }
    }
}
