using LIT.Smabu.Domain.OfferAggregate;
using LIT.Smabu.Shared.Interfaces;
using LIT.Smabu.UseCases.Offers;

namespace LIT.Smabu.UseCases.Offers.Get
{
    public record GetOfferQuery : IQuery<OfferDTO>
    {
        public GetOfferQuery(OfferId id)
        {
            Id = id;
        }

        public OfferId Id { get; }
    }
}