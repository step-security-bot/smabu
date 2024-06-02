using LIT.Smabu.Domain.Common;
using LIT.Smabu.Domain.InvoiceAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LIT.Smabu.Web.Areas.Invoices.Pages
{
    public class CreateModel(IMediator mediator) : PageModel
    {
        public List<SelectListItem> Customers { get; private set; }

        [BindProperty]
        public Guid CustomerId { get; set; }

        [BindProperty]
        public int FiscalYear { get; set; }

        [BindProperty]
        public string Currency { get; set; }

        public async Task OnGetAsync()
        {
            Currency = Domain.Common.Currency.GetEuro().ToString();
            FiscalYear = DateTime.Now.Year;
            Customers = [.. (await mediator.Send(new UseCases.Customers.List.ListCustomersQuery())).Select(x =>
                new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = $"{x.Name} ({x.DisplayName})",
                }).OrderBy(x => x.Text)];
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var result = await mediator.Send(new UseCases.Invoices.Create.CreateInvoiceCommand()
            {
                Id = new InvoiceId(Guid.NewGuid()),
                CustomerId = new(CustomerId),
                FiscalYear = FiscalYear,
                Currency = Domain.Common.Currency.GetEuro(),
                PerformancePeriod = DatePeriod.CreateFrom(DateTime.Now, null),
                Tax = 0,
                TaxDetails = "Umsatzsteuer wird aufgrund der Befreiung für Kleinunternehmer gemäß § 19 Abs. 1 UStG nicht gesondert ausgewiesen."
            });

            if (result != null)
            {
                return RedirectToPage("Details", new { result.Id });
            }

            return Page();
        }
    }
}
