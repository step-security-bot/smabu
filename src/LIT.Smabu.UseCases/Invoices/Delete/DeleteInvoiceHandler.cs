using LIT.Smabu.Shared;
using LIT.Smabu.UseCases.SeedWork;

namespace LIT.Smabu.UseCases.Invoices.Delete
{
    public class DeleteInvoiceHandler(IAggregateStore aggregateStore) : ICommandHandler<DeleteInvoiceCommand>
    {
        public async Task<Result> Handle(DeleteInvoiceCommand request, CancellationToken cancellationToken)
        {
            var invoice = await aggregateStore.GetByAsync(request.Id);
            var result = invoice.Delete();
            if (result.IsFailure)
            {
                return result.Error;
            }

            await aggregateStore.DeleteAsync(invoice);
            return Result.Success();
        }
    }
}
