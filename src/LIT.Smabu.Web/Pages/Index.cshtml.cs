using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LIT.Smabu.Web.Pages
{
    public class IndexModel() : PageModel
    {
        public Task OnGetAsync()
        {
            return Task.CompletedTask;
        }
    }
}
