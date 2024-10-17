using LIT.Smabu.Domain.SeedWork;
using LIT.Smabu.UseCases.SeedWork;

namespace LIT.Smabu.UseCases.Invoices.CreateReport
{
    public class GetInvoiceReportHandler(IReportService reportService) : IQueryHandler<GetInvoiceReportQuery, IReport>
    {
        public async Task<Result<IReport>> Handle(GetInvoiceReportQuery request, CancellationToken cancellationToken)
        {
            var report = await reportService.CreateInvoiceReportAsync(request.Id);
            return Result<IReport>.Success(report);
        }
    }
}
