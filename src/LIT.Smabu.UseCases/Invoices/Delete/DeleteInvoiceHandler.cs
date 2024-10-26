using LIT.Smabu.Domain.Shared;
using LIT.Smabu.Shared;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Invoices.Delete
{
    public class DeleteInvoiceHandler(IAggregateStore store) : ICommandHandler<DeleteInvoiceCommand>
    {
        public async Task<Result> Handle(DeleteInvoiceCommand request, CancellationToken cancellationToken)
        {
            var invoice = await store.GetByAsync(request.Id);
            var result = invoice.Delete();
            if (result.IsFailure)
            {
                return result.Error;
            }

            await store.DeleteAsync(invoice);
            return Result.Success();
        }
    }
}
