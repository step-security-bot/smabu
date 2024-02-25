using LIT.Smabu.Domain.OfferAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LIT.Smabu.Web.Areas.Offers.Pages
{
    public class DeleteModel(IMediator mediator) : PageModel
    {
        public string DisplayName { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        public async Task OnGetAsync(Guid id)
        {
            var offer = await mediator.Send(new UseCases.Offers.Get.GetOfferQuery(new(id)));
            this.Id = offer.Id.Value;
            this.DisplayName = offer.DisplayName;
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            var result = await mediator.Send(new UseCases.Offers.Delete.DeleteOfferCommand() { Id = new(id) });

            if (result)
            {
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
