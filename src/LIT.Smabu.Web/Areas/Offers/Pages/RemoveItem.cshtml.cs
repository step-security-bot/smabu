using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LIT.Smabu.Web.Areas.Offers.Pages
{
    public class RemoveItemModel(IMediator mediator) : PageModel
    {
        public string DisplayName { get; set; }
        public Guid Id { get; set; }
        public Guid OfferId { get; private set; }

        public async Task OnGetAsync(Guid id, Guid offerId)
        {
            var offer = await mediator.Send(new UseCases.Offers.GetWithItems.GetOfferWithItemsQuery(new(offerId)));
            var item = offer.Items.Find(x => x.Id.Equals(id));
            this.Id = id;
            this.OfferId = offerId;
            this.DisplayName = offer.DisplayName + " Pos. " + item.DisplayName;
        }

        public async Task<IActionResult> OnPostAsync(Guid id, Guid offerId)
        {
            var result = await mediator.Send(new UseCases.Offers.RemoveOfferItem.RemoveOfferItemCommand() { Id = new(id), OfferId = new(offerId) });

            if (result)
            {
                return RedirectToPage("Details", new { Id = offerId });
            }

            return Page();
        }
    }
}
