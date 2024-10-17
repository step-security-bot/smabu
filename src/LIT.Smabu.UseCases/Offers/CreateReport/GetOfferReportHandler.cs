using LIT.Smabu.Domain.Shared;
using LIT.Smabu.UseCases.SeedWork;

namespace LIT.Smabu.UseCases.Offers.CreateReport
{
    public class GetInvoiceReportHandler(IReportFactory reportService) : IQueryHandler<GetOfferReportQuery, IReport>
    {
        public async Task<Result<IReport>> Handle(GetOfferReportQuery request, CancellationToken cancellationToken)
        {
            var report = await reportService.CreateOfferReportAsync(request.Id);
            return Result<IReport>.Success(report);
        }
    }
}
