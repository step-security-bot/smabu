using LIT.Smabu.Domain.Shared.Invoices;
using LIT.Smabu.Domain.Shared.Invoices.Commands;
using LIT.Smabu.Infrastructure.CQRS;
using LIT.Smabu.Infrastructure.DDD;

namespace LIT.Smabu.Business.Service.Customers.Queries
{
    public class AddInvoiceLineHandler : RequestHandler<AddInvoiceLineCommand, InvoiceLineId>
    {
        public AddInvoiceLineHandler(IAggregateStore aggregateStore) : base(aggregateStore)
        {

        }

        public override async Task<InvoiceLineId> Handle(AddInvoiceLineCommand request, CancellationToken cancellationToken)
        {
            var invoice = await this.AggregateStore.GetAsync(request.InvoiceId);
            var invoiceLine = invoice.AddInvoiceLine(request.Details, request.Quantity, request.UnitPrice); 
            await this.AggregateStore.AddOrUpdateAsync(invoice);
            return invoiceLine.Id;
        }
    }
}
