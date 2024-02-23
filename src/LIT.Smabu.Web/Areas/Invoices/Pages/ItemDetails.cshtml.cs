using LIT.Smabu.Domain.InvoiceAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LIT.Smabu.Web.Areas.Invoices.Pages
{
    public class ItemDetailsModel(IMediator mediator) : PageModel
    {
        public string DisplayName { get; private set; }
        [BindProperty]
        public Guid Id { get; set; }
        [BindProperty]
        public Guid InvoiceId { get; set; }
        [BindProperty]
        public decimal QuantityValue { get; set; }
        [BindProperty]
        public string QuantityUnit { get; set; }
        [BindProperty]
        public decimal UnitPrice { get; set; }
        [BindProperty]
        public string Details { get; set; }
        public decimal TotalPrice { get; private set; }

        public async Task OnGetAsync(Guid id, Guid invoiceId)
        {
            var invoice = await mediator.Send(new UseCases.Invoices.GetWithItems.GetInvoiceWithItemsQuery(new InvoiceId(invoiceId)));
            var invoiceItem = invoice.Items.Find(x => x.Id == new InvoiceItemId(id));

            this.DisplayName = invoice.DisplayName + " Pos. " + invoiceItem.DisplayName;
            this.Id = invoiceItem.Id.Value;
            this.InvoiceId = invoice.Id.Value;
            this.Details = invoiceItem.Details;
            this.QuantityValue = invoiceItem.Quantity.Value;
            this.QuantityUnit = invoiceItem.Quantity.Unit;
            this.UnitPrice = invoiceItem.UnitPrice;
            this.TotalPrice = invoiceItem.TotalPrice;
        }

        public async Task<IActionResult> OnPostAsync(Guid id, Guid invoiceId)
        {
            var result = await mediator.Send(new UseCases.Invoices.UpdateInvoiceItem.UpdateInvoiceItemCommand()
            {
                Id = new InvoiceItemId(id),
                InvoiceId = new InvoiceId(invoiceId),
                Details = this.Details,
                Quantity = new(this.QuantityValue, this.QuantityUnit),
                UnitPrice = this.UnitPrice
            });

            if(result != null)
            {
                return RedirectToPage(new { id, invoiceId });
            }

            return Page();
        }
    }
}
