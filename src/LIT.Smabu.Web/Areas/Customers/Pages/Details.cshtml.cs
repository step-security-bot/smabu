using LIT.Smabu.Domain.Common;
using LIT.Smabu.Domain.CustomerAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LIT.Smabu.Web.Areas.Customers.Pages
{
    public class DetailsModel(IMediator mediator) : PageModel
    {
        public string DisplayName { get; set; }
        public string Number { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string IndustryBranch { get; set; }

        [BindProperty]
        public string ShortName { get; set; }

        [BindProperty]
        public string Currency { get; set; }

        [BindProperty]
        public string MainAddressName1 { get; set; }

        [BindProperty]
        public string MainAddressName2 { get; set; }

        [BindProperty]
        public string MainAddressStreet { get; set; }

        [BindProperty]
        public string MainAddressHouseNumber { get; set; }

        [BindProperty]
        public string MainAddressPostalCode { get; set; }

        [BindProperty]
        public string MainAddressCity { get; set; }

        [BindProperty]
        public string MainAddressCountry { get; set; }

        [BindProperty]
        public string CommunicationMobil { get; set; }

        [BindProperty]
        public string CommunicationEmail { get; set; }

        [BindProperty]
        public string CommunicationWebsite { get; set; }

        [BindProperty]
        public string CommunicationPhone { get; set; }

        public async Task OnGetAsync(Guid id)
        {
            var customer = await mediator.Send(new UseCases.Customers.Get.GetCustomerQuery(new CustomerId(id)));
            Id = customer.Id.Value;
            DisplayName = customer.DisplayName;
            Number = customer.Number.Long;
            Name = customer.Name;
            Currency = customer.Currency.ToString();
            IndustryBranch = customer.IndustryBranch;
            ShortName = customer.ShortName;
            MainAddressName1 = customer.MainAddress.Name1;
            MainAddressName2 = customer.MainAddress.Name2;
            MainAddressStreet = customer.MainAddress.Street;
            MainAddressHouseNumber = customer.MainAddress.HouseNumber;
            MainAddressPostalCode = customer.MainAddress.PostalCode;
            MainAddressCity = customer.MainAddress.City;
            MainAddressCountry = customer.MainAddress.Country;
            CommunicationMobil = customer.Communication.Mobil;
            CommunicationEmail = customer.Communication.Email;
            CommunicationWebsite = customer.Communication.Website;
            CommunicationPhone = customer.Communication.Phone;
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            var result = await mediator.Send(new UseCases.Customers.Update.UpdateCustomerCommand()
            {
                Id = new CustomerId(id),
                Name = Name,
                IndustryBranch = IndustryBranch,
                MainAddress = new Address(MainAddressName1, MainAddressName2, MainAddressStreet, MainAddressHouseNumber, 
                    MainAddressPostalCode, MainAddressCity, MainAddressCountry),
                Communication = new Communication(CommunicationEmail, CommunicationMobil, CommunicationPhone, CommunicationWebsite)
            });

            if (result != null)
            {
                return RedirectToPage(new { id });
            }

            return Page();
        }
    }
}
