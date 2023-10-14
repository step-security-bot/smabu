using LIT.Smabu.Domain.Shared.Common;
using LIT.Smabu.Domain.Shared.Customers;
using LIT.Smabu.Domain.Shared.Offers;
using LIT.Smabu.Shared.Offers;
using MediatR;

namespace LIT.Smabu.Business.Service.Offers.Commands
{
    public record CreateOfferCommand : IRequest<OfferDTO>
    {
        public required OfferId Id { get; set; }
        public required CustomerId CustomerId { get; set; }
        public required Currency Currency { get; set; }
        public decimal Tax { get; set; }
        public string TaxDetails { get; set; }
        public OfferNumber? Number { get; set; }
    }
}
