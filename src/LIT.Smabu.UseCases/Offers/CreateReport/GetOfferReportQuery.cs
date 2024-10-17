using LIT.Smabu.Domain.OfferAggregate;
using LIT.Smabu.Domain.Shared;
using LIT.Smabu.UseCases.SeedWork;

namespace LIT.Smabu.UseCases.Offers.CreateReport
{
    public record GetOfferReportQuery(OfferId Id) : IQuery<IReport>
    {

    }
}