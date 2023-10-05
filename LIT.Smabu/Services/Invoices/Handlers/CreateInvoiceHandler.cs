using LIT.Smabu.Infrastructure.CQRS;
using LIT.Smabu.Infrastructure.DDD;
using LIT.Smabu.Shared.Domain.Common;
using LIT.Smabu.Shared.Domain.Customers;
using LIT.Smabu.Shared.Domain.Customers.Commands;
using LIT.Smabu.Shared.Domain.Invoices;
using LIT.Smabu.Shared.Domain.Offers;
using LIT.Smabu.Shared.Domain.Orders;
using LIT.Smabu.Shared.Domain.Products;

namespace LIT.Smabu.Business.Service.Customers.Queries
{
    public class CreateInvoiceHandler : RequestHandler<CreateInvoiceCommand, InvoiceId>
    {
        public CreateInvoiceHandler(IAggregateStore aggregateStore) : base(aggregateStore)
        {

        }

        public override async Task<InvoiceId> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            request.Number ??= CreateNewNumber(request.PerformancePeriod.To.Year);
            var invoice = Invoice.Create(request.Id, request.CustomerId, request.Number, request.PerformancePeriod,
                request.Currency, request.Tax, request.TaxDetails, request.OrderId, request.OfferId);
            await this.AggregateStore.AddOrUpdateAsync(invoice);
            return invoice.Id;
        }

    //    public async Task<Invoice> AddInvoiceLineAsync(InvoiceId invoiceId, string details, Quantity quantity, decimal unitPrice, ProductId? productId = null)
    //    {
    //        var invoice = aggregateStore.Get(invoiceId);
    //        invoice.AddInvoiceLine(details, quantity, unitPrice, productId);
    //        await aggregateStore.AddOrUpdateAsync(invoice);
    //        return invoice;
    //    }

        private InvoiceNumber CreateNewNumber(int year)
        {
            var lastNumber = this.AggregateStore.GetAll<Invoice>()
                .Select(x => x.Number)
                .OrderByDescending(x => x)
                .FirstOrDefault();

            return lastNumber == null ? InvoiceNumber.CreateFirst(year) : InvoiceNumber.CreateNext(lastNumber);
        }
    }
}
