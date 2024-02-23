using LIT.Smabu.UseCases.Customers;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LIT.Smabu.Web.Areas.Customers.Pages
{
    public class IndexModel(IMediator mediator) : PageModel
    {
        private readonly IMediator mediator = mediator;

        public CustomerDTO[] Source { get; private set; }

        public async Task OnGetAsync()
        {
            Source = await mediator.Send(new UseCases.Customers.List.ListCustomersQuery());
        }
    }
}
