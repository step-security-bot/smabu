using LIT.Smabu.Domain.Common;
using LIT.Smabu.Domain.InvoiceAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LIT.Smabu.Web.Areas.Invoices.Pages
{
    public class AddItemModel(IMediator mediator) : PageModel
    {
        public string DisplayName { get; private set; }
        public List<SelectListItem> QuantityUnits { get; private set; }

        [BindProperty]
        public Guid InvoiceId { get; set; }
        [BindProperty]
        public string Details { get; set; }
        [BindProperty]
        public decimal QuantityValue { get; set; }
        [BindProperty]
        public string QuantityUnit { get; set; }
        [BindProperty]
        public decimal UnitPrice { get; set; }

        public async Task OnGetAsync(Guid invoiceId)
        {
            this.InvoiceId = invoiceId;
            var invoice = await mediator.Send(new UseCases.Invoices.Get.GetInvoiceQuery(new InvoiceId(invoiceId)));
            this.DisplayName = invoice.DisplayName;
            this.QuantityUnits = Quantity.GetUnits().Select(x => new SelectListItem(x, x)).ToList();
        }

        public async Task<IActionResult> OnPostAsync(Guid invoiceId)
        {
            var result = await mediator.Send(new UseCases.Invoices.AddInvoiceItem.AddInvoiceItemCommand()
            {
                Id = new InvoiceItemId(Guid.NewGuid()),
                Details = this.Details,
                InvoiceId = new(invoiceId),
                Quantity = new Domain.Common.Quantity(this.QuantityValue, this.QuantityUnit),
                UnitPrice = this.UnitPrice
            });

            if (result != null)
            {
                return RedirectToPage("Details", new { Id = invoiceId });
            }

            return Page();
        }
    }
}
