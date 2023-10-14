using LIT.Smabu.Domain.Shared.Offers;
using LIT.Smabu.Shared.Contracts;

namespace LIT.Smabu.Shared.Offers
{
    public class EditOfferDTO : IDTO
    {
        public required OfferId Id { get; set; }
        public DateTime OfferDate { get; set; }
        public DateTime ExpiresOn { get; set; }
        public decimal Tax { get; set; }
        public string TaxDetails { get; set; }
    }
}
