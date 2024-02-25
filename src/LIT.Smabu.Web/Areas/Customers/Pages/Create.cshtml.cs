using LIT.Smabu.Domain.CustomerAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LIT.Smabu.Web.Areas.Customers.Pages
{
    public class CreateModel(IMediator mediator) : PageModel
    {
        [BindProperty]
        public string Name { get; set; }

        public Task OnGetAsync()
        {
            return Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var result = await mediator.Send(new UseCases.Customers.Create.CreateCustomerCommand()
            {
                Id = new CustomerId(Guid.NewGuid()),
                Name = Name
            });

            if (result != null)
            {
                return RedirectToPage("Details", new { Id = result.Value });
            }

            return Page();
        }
    }
}
