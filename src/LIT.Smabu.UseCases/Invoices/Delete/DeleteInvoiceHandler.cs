using LIT.Smabu.Shared.Interfaces;
using MediatR;

namespace LIT.Smabu.UseCases.Invoices.Delete
{
    public class DeleteInvoiceHandler : IRequestHandler<DeleteInvoiceCommand, bool>
    {
        private readonly IAggregateStore aggregateStore;

        public DeleteInvoiceHandler(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

        public async Task<bool> Handle(DeleteInvoiceCommand request, CancellationToken cancellationToken)
        {
            var invoice = await aggregateStore.GetByAsync(request.Id);
            invoice.Delete();
            await aggregateStore.DeleteAsync(invoice);
            return true;
        }
    }
}
