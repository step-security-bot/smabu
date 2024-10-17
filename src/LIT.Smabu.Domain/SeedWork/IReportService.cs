using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.Domain.OfferAggregate;

namespace LIT.Smabu.Domain.SeedWork
{
    public interface IReportService
    {
        Task<IReport> CreateInvoiceReportAsync(InvoiceId id);
        Task<IReport> CreateOfferReportAsync(OfferId id);
    }
}
