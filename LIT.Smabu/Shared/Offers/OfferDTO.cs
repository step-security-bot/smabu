using LIT.Smabu.Domain.Shared.Common;
using LIT.Smabu.Domain.Shared.Offers;
using LIT.Smabu.Shared.Contracts;
using LIT.Smabu.Shared.Customers;

namespace LIT.Smabu.Shared.Offers
{
    public class OfferDTO : IDTO
    {
        public OfferId Id { get; set; }
        public CustomerDTO Customer { get; set; }
        public OfferNumber Number { get; set; }
        public DateTime OfferDate { get; set; }
        public DateTime ExpiresOn { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public decimal Tax { get; set; }
        public string TaxDetails { get; set; }
        public List<OfferItemDTO> Items { get; set; }
    }
}