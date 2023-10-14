using LIT.Smabu.Domain.Shared.Offers;
using MediatR;

namespace LIT.Smabu.Business.Service.Offers.Commands
{
    public record RemoveOfferItemCommand : IRequest
    {
        public required OfferItemId Id { get; set; }
        public required OfferId OfferId { get; set; }
    }
}
