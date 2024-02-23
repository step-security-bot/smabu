using LIT.Smabu.Domain.Common;
using LIT.Smabu.Domain.OfferAggregate;
using LIT.Smabu.UseCases.Offers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LIT.Smabu.Web.Areas.Offers.Pages
{
    public class DetailsModel(IMediator mediator) : PageModel
    {
        [BindProperty]
        public Guid Id { get; set; }
        [BindProperty]
        public string Number { get; set; }
        [BindProperty]
        public decimal Tax { get; set; }
        [BindProperty]
        public string TaxDetails { get; set; }

        public string Customer { get; private set; }
        public string DisplayName { get; private set; }
        public Currency Currency { get; private set; }
        public List<OfferItemDTO> Items { get; private set; }

        public async Task OnGetAsync(Guid id)
        {
            var invoice = await mediator.Send(new UseCases.Offers.GetWithItems.GetOfferWithItemsQuery(new OfferId(id)));
            Id = invoice.Id.Value;
            Number = invoice.Number.Long;
            Tax = invoice.Tax;
            TaxDetails = invoice.TaxDetails;
            Customer = invoice.Customer.DisplayName;
            DisplayName = invoice.DisplayName;
            Currency = invoice.Currency;
            Items = invoice.Items;
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            var result = await mediator.Send(new UseCases.Offers.Update.UpdateOfferCommand()
            {
                Id = new OfferId(id),
                Tax = Tax,
                TaxDetails = TaxDetails
            });

            if(result != null)
            {
                return RedirectToPage(new { id });
            }

            return Page();
        }
    }
}
