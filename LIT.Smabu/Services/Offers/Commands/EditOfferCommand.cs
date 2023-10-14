using LIT.Smabu.Domain.Shared.Offers;
using LIT.Smabu.Shared.Offers;
using MediatR;

namespace LIT.Smabu.Business.Service.Offers.Commands
{
    public record EditOfferCommand : IRequest<OfferDTO>
    {
        public required OfferId Id { get; set; }
        public required decimal Tax { get; set; }
        public required string TaxDetails { get; set; }
        public DateOnly OfferDate { get; set; }
        public DateOnly ExpiresOn { get; set; }
    }
}
