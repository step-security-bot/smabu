using LIT.Smabu.Domain.Common;
using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.UseCases.Invoices;
using LIT.Smabu.Web.Areas.Invoices.Documents;
using LIT.Smabu.Web.Pages.Shared.Documents;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuestPDF.Fluent;


namespace LIT.Smabu.Web.Areas.Invoices.Pages
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
        [BindProperty]
        public DateTime PerformancePeriodFrom { get; set; }
        [BindProperty]
        public DateTime? PerformancePeriodTo { get; set; }

        public string Customer { get; private set; }
        public int FiscalYear { get; set; }
        public bool IsReleased { get; private set; }
        public string ReleasedOn { get; private set; }
        public string DisplayName { get; private set; }
        public Currency Currency { get; private set; }
        public List<InvoiceItemDTO> Items { get; private set; }

        public async Task OnGetAsync(Guid id)
        {
            var invoice = await mediator.Send(new UseCases.Invoices.GetWithItems.GetInvoiceWithItemsQuery(new(id)));
            Id = invoice.Id.Value;
            Number = invoice.Number.Long;
            Tax = invoice.Tax;
            TaxDetails = invoice.TaxDetails;
            Customer = invoice.Customer.DisplayName;
            FiscalYear = invoice.FiscalYear;
            PerformancePeriodFrom = invoice.PerformancePeriod.From.ToDateTime(TimeOnly.MinValue);
            PerformancePeriodTo = invoice.PerformancePeriod.To?.ToDateTime(TimeOnly.MinValue);
            IsReleased = invoice.IsReleased;
            ReleasedOn = invoice.ReleasedOn?.ToShortDateString();
            DisplayName = invoice.DisplayName;
            Currency = invoice.Currency;
            Items = invoice.Items;
        }

        public async Task<IActionResult> OnPostReleaseAsync(Guid id)
        {
            await mediator.Send(new UseCases.Invoices.Release.ReleaseInvoiceCommand() { Id = new(id), Number = null, ReleasedOn = DateTime.Now });
            return RedirectToPage(new { id });
        }

        public async Task<IActionResult> OnPostDownloadPDFAsync(Guid id)
        {
            var invoice = await mediator.Send(new UseCases.Invoices.GetWithItems.GetInvoiceWithItemsQuery(new(id)));
            var invoiceDocument = new InvoiceDocument(invoice);
            return File(invoiceDocument.GeneratePdf(), "application/pdf", Utils.CreateFileNamePDF(invoice));
        }

        public async Task<IActionResult> OnPostWithdrawReleaseAsync(Guid id)
        {
            await mediator.Send(new UseCases.Invoices.WithdrawRelease.WithdrawReleaseInvoiceCommand() { Id = new InvoiceId(id) });
            return RedirectToPage(new { id });
        }

        public async Task<IActionResult> OnPostMoveDownAsync(Guid id, Guid itemId)
        {
            await mediator.Send(new UseCases.Invoices.MoveInvoiceItem.MoveInvoiceItemDownCommand(new(itemId), new(id)));
            return RedirectToPage(new { Id = id});
        }

        public async Task<IActionResult> OnPostMoveUpAsync(Guid id, Guid itemId)
        {
            await mediator.Send(new UseCases.Invoices.MoveInvoiceItem.MoveInvoiceItemUpCommand(new(itemId), new(id)));
            return RedirectToPage(new { Id = id });
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            var result = await mediator.Send(new UseCases.Invoices.Update.UpdateInvoiceCommand()
            {
                Id = new InvoiceId(id),
                PerformancePeriod = DatePeriod.CreateFrom(PerformancePeriodFrom, PerformancePeriodTo),
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
