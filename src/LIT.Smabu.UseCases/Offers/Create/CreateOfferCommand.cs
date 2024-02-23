using LIT.Smabu.Domain.Common;
using LIT.Smabu.Domain.CustomerAggregate;
using LIT.Smabu.Domain.OfferAggregate;
using LIT.Smabu.Shared.Interfaces;

namespace LIT.Smabu.UseCases.Offers.Create
{
    public record CreateOfferCommand : ICommand<OfferDTO>
    {
        public required OfferId Id { get; set; }
        public required CustomerId CustomerId { get; set; }
        public required Currency Currency { get; set; }
        public decimal Tax { get; set; }
        public string TaxDetails { get; set; }
        public OfferNumber? Number { get; set; }
    }
}
