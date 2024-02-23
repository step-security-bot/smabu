using LIT.Smabu.Domain.OfferAggregate;
using LIT.Smabu.Shared.Interfaces;

namespace LIT.Smabu.UseCases.Offers.Update
{
    public record UpdateOfferCommand : ICommand<OfferDTO>
    {
        public required OfferId Id { get; set; }
        public required decimal Tax { get; set; }
        public required string TaxDetails { get; set; }
        public DateOnly OfferDate { get; set; }
        public DateOnly ExpiresOn { get; set; }
    }
}
