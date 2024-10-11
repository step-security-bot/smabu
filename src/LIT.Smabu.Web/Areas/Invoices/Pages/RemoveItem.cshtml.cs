using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LIT.Smabu.Web.Areas.Invoices.Pages
{
    public class RemoveItemModel(IMediator mediator) : PageModel
    {
        public string DisplayName { get; set; }

        public Guid Id { get; set; }
        public Guid InvoiceId { get; private set; }

        public async Task OnGetAsync(Guid id, Guid invoiceId)
        {
            var invoice = await mediator.Send(new UseCases.Invoices.Get.GetInvoiceQuery(new(invoiceId)) { WithItems = true });
            var item = invoice.Items.Find(x => x.Id.Equals(id));
            this.Id = id;
            this.InvoiceId = invoiceId;
            this.DisplayName = invoice.DisplayName + " Pos. " + item.DisplayName;
        }

        public async Task<IActionResult> OnPostAsync(Guid id, Guid invoiceId)
        {
            var result = await mediator.Send(new UseCases.Invoices.RemoveInvoiceItem.RemoveInvoiceItemCommand(new(id), new(invoiceId)));

            if (result)
            {
                return RedirectToPage("Details", new { Id = invoiceId });
            }

            return Page();
        }
    }
}
