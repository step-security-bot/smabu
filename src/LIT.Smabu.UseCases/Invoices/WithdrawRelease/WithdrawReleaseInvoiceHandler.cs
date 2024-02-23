using LIT.Smabu.Shared.Interfaces;
using LIT.Smabu.UseCases.Invoices;

namespace LIT.Smabu.UseCases.Invoices.WithdrawRelease
{
    public class WithdrawReleaseInvoiceHandler : ICommandHandler<WithdrawReleaseInvoiceCommand, InvoiceDTO>
    {
        private readonly IAggregateStore aggregateStore;

        public WithdrawReleaseInvoiceHandler(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

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
