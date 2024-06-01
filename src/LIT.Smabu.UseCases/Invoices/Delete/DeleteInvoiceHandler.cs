using LIT.Smabu.Domain.SeedWork;
using LIT.Smabu.UseCases.SeedWork;

namespace LIT.Smabu.UseCases.Invoices.Delete
{
    public class DeleteInvoiceHandler(IAggregateStore aggregateStore) : ICommandHandler<DeleteInvoiceCommand, bool>
    {
        public async Task<bool> Handle(DeleteInvoiceCommand request, CancellationToken cancellationToken)
        {
            var invoice = await aggregateStore.GetByAsync(request.Id);
            invoice.Delete();
            await aggregateStore.DeleteAsync(invoice);
            return true;
        }
    }
}
