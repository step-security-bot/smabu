using LIT.Smabu.Domain.Shared.Invoices;
using LIT.Smabu.Infrastructure.CQRS;
using LIT.Smabu.Infrastructure.DDD;

namespace LIT.Smabu.Business.Service.Invoices.Commands
{
    public class AddInvoiceLineHandler : RequestHandler<AddInvoiceLineCommand, InvoiceLineId>
    {
        public AddInvoiceLineHandler(IAggregateStore aggregateStore) : base(aggregateStore)
        {

        }

        public override async Task<InvoiceLineId> Handle(AddInvoiceLineCommand request, CancellationToken cancellationToken)
        {
            var invoice = await AggregateStore.GetAsync(request.InvoiceId);
            var invoiceLine = invoice.AddInvoiceLine(request.Details, request.Quantity, request.UnitPrice);
            await AggregateStore.AddOrUpdateAsync(invoice);
            return invoiceLine.Id;
        }
    }
}
