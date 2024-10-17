using LIT.Smabu.Shared;
using LIT.Smabu.UseCases.SeedWork;

namespace LIT.Smabu.UseCases.Invoices.WithdrawRelease
{
    public class WithdrawReleaseInvoiceHandler(IAggregateStore aggregateStore) : ICommandHandler<WithdrawReleaseInvoiceCommand>
    {
        public async Task<Result> Handle(WithdrawReleaseInvoiceCommand request, CancellationToken cancellationToken)
        {
            var invoice = await aggregateStore.GetByAsync(request.Id);
            var result = invoice.WithdrawRelease();
            if (result.IsFailure)
            {
                return result.Error;
            }

            await aggregateStore.UpdateAsync(invoice);
            return Result.Success();
        }
    }
}
