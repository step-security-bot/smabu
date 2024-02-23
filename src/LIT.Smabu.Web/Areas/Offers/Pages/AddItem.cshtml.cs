using LIT.Smabu.Domain.Common;
using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.Domain.OfferAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LIT.Smabu.Web.Areas.Offers.Pages
{
    public class AddItemModel(IMediator mediator) : PageModel
    {
        public string DisplayName { get; private set; }
        public List<SelectListItem> QuantityUnits { get; private set; }

        [BindProperty]
        public Guid OfferId { get; set; }
        [BindProperty]
        public string Details { get; set; }
        [BindProperty]
        public decimal QuantityValue { get; set; }
        [BindProperty]
        public string QuantityUnit { get; set; }
        [BindProperty]
        public decimal UnitPrice { get; set; }

        public async Task OnGetAsync(Guid offerId)
        {
            this.OfferId = offerId;
            var offer = await mediator.Send(new UseCases.Offers.Get.GetOfferQuery(new OfferId(offerId)));
            this.DisplayName = offer.DisplayName;
            this.QuantityUnits = Quantity.GetUnits().Select(x => new SelectListItem(x, x)).ToList();
        }

        public async Task<IActionResult> OnPostAsync(Guid offerId)
        {
            var result = await mediator.Send(new UseCases.Offers.AddOfferItem.AddOfferItemCommand()
            {
                Id = new OfferItemId(Guid.NewGuid()),
                Details = this.Details,
                OfferId = new(offerId),
                Quantity = new Domain.Common.Quantity(this.QuantityValue, this.QuantityUnit),
                UnitPrice = this.UnitPrice
            });

            if (result != null)
            {
                return RedirectToPage("Details", new { Id = offerId });
            }

            return Page();
        }
    }
}
