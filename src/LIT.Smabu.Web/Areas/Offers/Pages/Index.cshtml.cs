using LIT.Smabu.UseCases.Offers;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LIT.Smabu.Web.Areas.Offers.Pages
{
    public class IndexModel(IMediator mediator) : PageModel
    {
        private readonly IMediator mediator = mediator;

        public OfferDTO[] Source { get; private set; }

        public async Task OnGetAsync()
        {
            Source = await mediator.Send(new UseCases.Offers.List.ListOffersQuery());
        }
    }
}
