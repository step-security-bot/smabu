using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LIT.Smabu.Web.Areas.Invoices.Pages
{
    public class DeleteModel(IMediator mediator) : PageModel
    {
        public string DisplayName { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        public async Task OnGetAsync(Guid id)
        {
            var invoice = await mediator.Send(new UseCases.Invoices.Get.GetInvoiceQuery(new(id)));
            this.Id = invoice.Id.Value;
            this.DisplayName = invoice.DisplayName;
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            var result = await mediator.Send(new UseCases.Invoices.Delete.DeleteInvoiceCommand() { Id = new(id) });

            if (result != null)
            {
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
