using LIT.Smabu.Business.Service.Customers.Commands;
using LIT.Smabu.Business.Service.Customers.Queries;
using LIT.Smabu.Domain.Shared.Customers;
using LIT.Smabu.Shared.Customers;
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
    public class CustomersController : ControllerBase
    {
        private readonly ISender sender;

        public CustomersController(ISender sender)
        {
            this.sender = sender;
        }

        [HttpGet("")]
        public async Task<CustomerDTO[]> Get() 
            => await this.sender.Send(new GetCustomersQuery());
       
        [HttpGet("{id}")]
        public async Task<CustomerDTO> GetDetails(Guid id) 
            => await this.sender.Send(new GetCustomerByIdQuery(new CustomerId(id)));
        
        [HttpPost]
        public async Task<CustomerId> Post([FromBody] CustomerDTO model) 
            => await this.sender.Send(new CreateCustomerCommand 
            {
                Id = model.Id,
                Name = model.Name,
                Number = model.Number,
            });

        [HttpPut("{id}")]
        public async Task<CustomerId> Put([FromBody] CustomerDTO model) 
            => await this.sender.Send(new EditCustomerCommand
            {
                Id = model.Id,
                Name = model.Name,
                IndustryBranch = model.IndustryBranch,
                MainAddress = model.MainAddress.ToValueObject(),
                Communication = model.Communication.ToValueObject()
            });
    }
}