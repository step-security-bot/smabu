using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.Domain.InvoiceAggregate.Specifications;
using LIT.Smabu.Shared;
using LIT.Smabu.UseCases.SeedWork;

namespace LIT.Smabu.UseCases.Invoices.Release
{
    public class ReleaseInvoiceHandler(IAggregateStore aggregateStore) : ICommandHandler<ReleaseInvoiceCommand>
    {
        public async Task<Result> Handle(ReleaseInvoiceCommand request, CancellationToken cancellationToken)
        {
            var invoice = await aggregateStore.GetByAsync(request.Id);
            InvoiceNumber number = await DetectOrCreateNumber(request, invoice);
            var result = invoice.Release(number, request.ReleasedOn);
            if (result.IsFailure)
            {
                return result.Error;
            }

            await aggregateStore.UpdateAsync(invoice);
            return Result.Success();
        }

        private async Task<InvoiceNumber> DetectOrCreateNumber(ReleaseInvoiceCommand request, Invoice invoice)
        {
            return invoice.Number.IsTemporary ? request.Number ?? await CreateNewNumberAsync(invoice.FiscalYear) : invoice.Number;
        }

        private async Task<InvoiceNumber> CreateNewNumberAsync(int year)
        {
            var lastInvoice = (await aggregateStore.ApplySpecification(new LastInvoiceInYearSpec(year))).SingleOrDefault();
            var lastNumber = lastInvoice?.Number;
            return lastNumber == null ? InvoiceNumber.CreateFirst(year) : InvoiceNumber.CreateNext(lastNumber);
        }
    }
}
