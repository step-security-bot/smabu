using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.Domain.SeedWork;
using LIT.Smabu.UseCases.SeedWork;

namespace LIT.Smabu.UseCases.Invoices.Update
{
    public class UpdateInvoiceHandler(IAggregateStore aggregateStore) : ICommandHandler<UpdateInvoiceCommand, InvoiceId>
    {
        public async Task<Result<InvoiceId>> Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var invoice = await aggregateStore.GetByAsync(request.Id);
            var result = invoice.Update(request.PerformancePeriod, request.TaxRate, request.InvoiceDate);
            if (result.IsSuccess)
            {
                await aggregateStore.UpdateAsync(invoice);
                return invoice.Id;
            }
            else
            {
                return result.Error;
            }
        }
    }
}
