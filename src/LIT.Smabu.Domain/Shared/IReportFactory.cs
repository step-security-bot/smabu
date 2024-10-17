using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.Domain.OfferAggregate;

namespace LIT.Smabu.Domain.Shared
{
    public interface IReportFactory
    {
        Task<IReport> CreateInvoiceReportAsync(InvoiceId id);
        Task<IReport> CreateOfferReportAsync(OfferId id);
    }
}
