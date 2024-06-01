using LIT.Smabu.Domain.SeedWork;
using LIT.Smabu.UseCases.SeedWork;

namespace LIT.Smabu.UseCases.Invoices.WithdrawRelease
{
    public class WithdrawReleaseInvoiceHandler(IAggregateStore aggregateStore) : ICommandHandler<WithdrawReleaseInvoiceCommand, InvoiceDTO>
    {
        public async Task<InvoiceDTO> Handle(WithdrawReleaseInvoiceCommand request, CancellationToken cancellationToken)
        {
            var invoice = await aggregateStore.GetByAsync(request.Id);
            var customer = await aggregateStore.GetByAsync(invoice.CustomerId);
            invoice.WithdrawRelease();
            await aggregateStore.UpdateAsync(invoice);
            return InvoiceDTO.From(invoice, customer);
        }
    }
}
