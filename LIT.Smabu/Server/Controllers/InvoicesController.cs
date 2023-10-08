using LIT.Smabu.Business.Service.Invoices.Queries;
using LIT.Smabu.Domain.Shared.Invoices;
using LIT.Smabu.Shared.Customers;
using LIT.Smabu.Shared.Invoices;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace LIT.Smabu.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class InvoicesController : ControllerBase
    {
        private readonly ISender sender;

        public InvoicesController(ISender sender)
        {
            this.sender = sender;
        }

        [HttpGet("")]
        public async Task<InvoiceDTO[]> Get() 
            => await this.sender.Send(new GetInvoicesQuery());

        [HttpGet("{id}")]
        public async Task<InvoiceDTO> GetDetails(Guid id)
            => await this.sender.Send(new GetInvoiceByIdQuery(new InvoiceId(id)));

        //[HttpPost]
        //public async Task<CustomerOverviewDto> Post([FromBody] CreateCustomer model)
        //{
        //    var customer = await this.customerService.CreateAsync(model.Name);
        //    return CustomerOverviewDto.From(customer);
        //}

        //[HttpPut]
        //public async Task<CustomerOverviewDto> Put([FromBody] EditCustomer model)
        //{
        //    var customer = await this.customerService.EditAsync(model.Id, model.Name, model.IndustryBranch);
        //    return CustomerOverviewDto.From(customer);
        //}
    }
}