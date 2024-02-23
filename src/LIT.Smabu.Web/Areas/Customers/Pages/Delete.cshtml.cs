using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LIT.Smabu.Web.Areas.Customers.Pages
{
    public class DeleteModel(IMediator mediator) : PageModel
    {
        public string DisplayName { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        public async Task OnGetAsync(Guid id)
        {
            var customer = await mediator.Send(new UseCases.Customers.Get.GetCustomerQuery(new(id)));
            this.Id = customer.Id.Value;
            this.DisplayName = customer.DisplayName;
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            var result = await mediator.Send(new UseCases.Customers.Delete.DeleteCustomerCommand() { Id = new(id) });

            if (result)
            {
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
