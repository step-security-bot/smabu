using LIT.Smabu.Domain.SeedWork;
using LIT.Smabu.UseCases.Invoices.Get;
using LIT.Smabu.UseCases.SeedWork;

namespace LIT.Smabu.UseCases.Invoices.CreateReport
{
    public class GetInvoiceReportHandler(IAggregateStore aggregateStore) : IQueryHandler<GetInvoiceReportQuery, InvoiceReport>
    {
        public async Task<Result<InvoiceReport>> Handle(GetInvoiceReportQuery request, CancellationToken cancellationToken)
        {
            var invoice = await aggregateStore.GetByAsync(request.Id);
            var customer = await aggregateStore.GetByAsync(invoice.CustomerId);
            var invoiceDTO = InvoiceDTO.Create(invoice, customer, true);

            var report = new InvoiceReport(invoiceDTO);
            return report;
        }
    }
}
