using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.Domain.OfferAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LIT.Smabu.Web.Areas.Offers.Pages
{
    public class ItemDetailsModel(IMediator mediator) : PageModel
    {
        public string DisplayName { get; private set; }
        [BindProperty]
        public Guid Id { get; set; }
        [BindProperty]
        public Guid OfferId { get; set; }
        [BindProperty]
        public decimal QuantityValue { get; set; }
        [BindProperty]
        public string QuantityUnit { get; set; }
        [BindProperty]
        public decimal UnitPrice { get; set; }
        [BindProperty]
        public string Details { get; set; }
        public decimal TotalPrice { get; private set; }

        public async Task OnGetAsync(Guid id, Guid offerId)
        {
            var offer = await mediator.Send(new UseCases.Offers.GetWithItems.GetOfferWithItemsQuery(new OfferId(offerId)));
            var offerItem = offer.Items.Find(x => x.Id == new OfferItemId(id));

            this.DisplayName = offer.DisplayName + " Pos. " + offerItem.DisplayName;
            this.Id = offerItem.Id.Value;
            this.OfferId = offer.Id.Value;
            this.Details = offerItem.Details;
            this.QuantityValue = offerItem.Quantity.Value;
            this.QuantityUnit = offerItem.Quantity.Unit;
            this.UnitPrice = offerItem.UnitPrice;
            this.TotalPrice = offerItem.TotalPrice;
        }

        public async Task<IActionResult> OnPostAsync(Guid id, Guid offerId)
        {
            var result = await mediator.Send(new UseCases.Offers.UpdateOfferItem.UpdateOfferItemCommand()
            {
                Id = new(id),
                OfferId = new(offerId),
                Details = this.Details,
                Quantity = new(this.QuantityValue, this.QuantityUnit),
                UnitPrice = this.UnitPrice
            });

            if(result != null)
            {
                return RedirectToPage(new { id, offerId });
            }

            return Page();
        }
    }
}
