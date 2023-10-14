using LIT.Smabu.Shared.Offers;
using MediatR;

namespace LIT.Smabu.Business.Service.Offers.Queries
{
    public record GetOffersQuery : IRequest<OfferDTO[]>
    {
    }
}
