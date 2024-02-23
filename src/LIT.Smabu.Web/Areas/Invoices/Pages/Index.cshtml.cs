using LIT.Smabu.UseCases.Invoices;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LIT.Smabu.Web.Areas.Invoices.Pages
{
    public class IndexModel(IMediator mediator) : PageModel
    {
        private readonly IMediator mediator = mediator;

        public InvoiceDTO[] Source { get; private set; }

        public async Task OnGetAsync()
        {
            Source = await mediator.Send(new UseCases.Invoices.List.ListInvoicesQuery());
        }
    }
}
