using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.Domain.OfferAggregate;
using LIT.Smabu.Domain.Shared;
using LIT.Smabu.UseCases.Invoices;
using LIT.Smabu.UseCases.Offers;

namespace LIT.Smabu.Infrastructure.Reports
{
    internal class QuestReportFactory(IAggregateStore aggregateStore, ReportsConfig config) : IReportFactory
    {
        public async Task<IReport> CreateInvoiceReportAsync(InvoiceId id)
        {
            var invoice = await aggregateStore.GetByAsync(id);
            var customer = await aggregateStore.GetByAsync(invoice.CustomerId);
            var invoiceDTO = InvoiceDTO.Create(invoice, customer, true);
            var report = new InvoiceReport(invoiceDTO, config);
            return new QuestReport(report);
        }

        public async Task<IReport> CreateOfferReportAsync(OfferId id)
        {
            var offer = await aggregateStore.GetByAsync(id);
            var customer = await aggregateStore.GetByAsync(offer.CustomerId);
            var invoiceDTO = OfferDTO.Create(offer, customer, true);
            var report = new OfferReport(invoiceDTO, config);
            return new QuestReport(report);
        }
    }
}
