using LIT.Smabu.Domain.OfferAggregate;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Offers.Get
{
    public record GetOfferQuery : IQuery<OfferDTO>
    {
        public GetOfferQuery(OfferId id)
        {
            Id = id;
        }

        public OfferId Id { get; }
        public bool WithItems { get; set; }
    }
}