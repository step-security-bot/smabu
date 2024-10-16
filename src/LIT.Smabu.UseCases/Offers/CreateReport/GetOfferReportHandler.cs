using LIT.Smabu.Domain.SeedWork;
using LIT.Smabu.UseCases.Offers;
using LIT.Smabu.UseCases.SeedWork;

namespace LIT.Smabu.UseCases.Offers.CreateReport
{
    public class GetInvoiceReportHandler(IAggregateStore aggregateStore) : IQueryHandler<GetOfferReportQuery, OfferReport>
    {
        public async Task<Result<OfferReport>> Handle(GetOfferReportQuery request, CancellationToken cancellationToken)
        {
            var offer = await aggregateStore.GetByAsync(request.Id);
            var customer = await aggregateStore.GetByAsync(offer.CustomerId);
            var offerDTO = OfferDTO.Create(offer, customer, true);

            var report = new OfferReport(offerDTO);
            return report;
        }
    }
}
