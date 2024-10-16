using LIT.Smabu.Domain.OfferAggregate;
using LIT.Smabu.UseCases.SeedWork;

namespace LIT.Smabu.UseCases.Offers.CreateReport
{
    public record GetOfferReportQuery(OfferId Id) : IQuery<OfferReport>
    {

    }
}