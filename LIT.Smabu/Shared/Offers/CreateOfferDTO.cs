using LIT.Smabu.Domain.Shared.Common;
using LIT.Smabu.Domain.Shared.Customers;
using LIT.Smabu.Domain.Shared.Offers;
using LIT.Smabu.Shared.Contracts;

namespace LIT.Smabu.Shared.Offers
{
    public record CreateOfferDTO : IDTO
    {
        public OfferId Id { get; set; }
        public CustomerId CustomerId { get; set; }
        public Currency Currency { get; set; }
    }
}
