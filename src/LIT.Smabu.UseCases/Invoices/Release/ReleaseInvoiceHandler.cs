using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.Domain.InvoiceAggregate.Specifications;
using LIT.Smabu.Domain.SeedWork;
using LIT.Smabu.UseCases.SeedWork;

namespace LIT.Smabu.UseCases.Invoices.Release
{
    public class ReleaseInvoiceHandler(IAggregateStore aggregateStore) : ICommandHandler<ReleaseInvoiceCommand, InvoiceDTO>
    {
        public async Task<Result<InvoiceDTO>> Handle(ReleaseInvoiceCommand request, CancellationToken cancellationToken)
        {
            var invoice = await aggregateStore.GetByAsync(request.Id);
            var customer = await aggregateStore.GetByAsync(invoice.CustomerId);
            InvoiceNumber number = await DetectOrCreateNumber(request, invoice);
            invoice.Release(number, request.ReleasedOn);
            await aggregateStore.UpdateAsync(invoice);
            return InvoiceDTO.From(invoice, customer);
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
