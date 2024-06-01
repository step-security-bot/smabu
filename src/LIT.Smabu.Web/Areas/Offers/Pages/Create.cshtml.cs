using LIT.Smabu.Domain.OfferAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LIT.Smabu.Web.Areas.Offers.Pages
{
    public class CreateModel(IMediator mediator) : PageModel
    {
        public List<SelectListItem> Customers { get; private set; }

        [BindProperty]
        public Guid CustomerId { get; set; }

        [BindProperty]
        public string Currency { get; set; }

        public async Task OnGetAsync()
        {
            Currency = Domain.Common.Currency.GetEuro().ToString();
            Customers = (await mediator.Send(new UseCases.Customers.List.ListCustomersQuery())).Select(x =>
                new SelectListItem($"{x.Name} ({x.DisplayName})", x.Id.ToString())).OrderBy(x => x.Text).ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var result = await mediator.Send(new UseCases.Offers.Create.CreateOfferCommand()
            {
                Id = new OfferId(Guid.NewGuid()),
                CustomerId = new(CustomerId),
                Currency = Domain.Common.Currency.GetEuro(),
                Tax = 0,
                TaxDetails = ""
            });

            if (result != null)
            {
                return RedirectToPage("Details", new { result.Id });
            }

            return Page();
        }
    }
}
