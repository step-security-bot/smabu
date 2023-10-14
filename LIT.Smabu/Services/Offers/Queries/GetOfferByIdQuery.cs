using LIT.Smabu.Domain.Shared.Offers;
using LIT.Smabu.Shared.Offers;
using MediatR;

namespace LIT.Smabu.Business.Service.Offers.Queries
{
    public record GetOfferByIdQuery : IRequest<OfferDTO>
    {
        public GetOfferByIdQuery(OfferId id)
        {
            Id = id;
        }

        public OfferId Id { get; }
    }
}