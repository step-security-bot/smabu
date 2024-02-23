using LIT.Smabu.Domain.Common;
using LIT.Smabu.Domain.CustomerAggregate;
using LIT.Smabu.Domain.OfferAggregate;
using LIT.Smabu.Shared.Interfaces;
using LIT.Smabu.UseCases.Customers;

namespace LIT.Smabu.UseCases.Offers
{
    public record OfferDTO : IDTO
    {
        public string DisplayName => Number.Long + "/" + Customer.ShortName + "/" + CreatedOn?.ToShortDateString();
        public OfferId Id { get; set; }
        public DateTime? CreatedOn { get; set; }
        public CustomerDTO Customer { get; set; }
        public OfferNumber Number { get; set; }
        public DateOnly OfferDate { get; set; }
        public DateOnly ExpiresOn { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public decimal Tax { get; set; }
        public string TaxDetails { get; set; }

        internal static OfferDTO CreateFrom(Offer offer, Customer customer)
        {
            return new()
            {
                Id = offer.Id,
                Customer = CustomerDTO.CreateFrom(customer),
                Number = offer.Number,
                OfferDate = offer.OfferDate,
                ExpiresOn = offer.ExpiresOn,
                Amount = offer.Amount,
                Currency = offer.Currency,
                Tax = offer.Tax,
                TaxDetails = offer.TaxDetails,
                CreatedOn = offer.Meta?.CreatedOn
            };
        }
    }
}