using Microsoft.AspNetCore.Mvc;

namespace LIT.Smabu.Web.Pages.Shared.Components.PageHeader
{
    public class PageHeader : ViewComponent
    {
        public PageHeader()
        {

        }

        public string Area { get; private set; }
        public string Page { get; private set; }
        public string Details { get; private set; }

        public IViewComponentResult Invoke(string area, string page, string details = "")
        {
            this.Area = area;
            this.Page = page;
            this.Details = details;
            return View(this);
        }
    }
}
