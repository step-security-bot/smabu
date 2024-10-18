namespace LIT.Smabu.Shared
{
    public interface IReportFactory
    {
        Task<IReport> CreateInvoiceReportAsync(IEntityId id);
        Task<IReport> CreateOfferReportAsync(IEntityId id);
    }
}
