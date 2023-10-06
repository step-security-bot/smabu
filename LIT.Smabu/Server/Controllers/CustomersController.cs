using LIT.Smabu.Domain.Shared.Customers;
using LIT.Smabu.Domain.Shared.Customers.Commands;
using LIT.Smabu.Domain.Shared.Customers.Queries;
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
        public async Task<GetAllCustomersResponse[]> Get() 
            => await this.sender.Send(new GetAllCustomersQuery());
       
        [HttpGet("{id}")]
        public async Task<GetCustomerDetailsResponse> GetDetails(Guid id) 
            => await this.sender.Send(new GetCustomerDetailsQuery(new CustomerId(id)));
        
        [HttpPost]
        public async Task<CustomerId> Post([FromBody] CreateCustomerCommand model) 
            => await this.sender.Send(model);

        [HttpPut("{id}")]
        public async Task<CustomerId> Put([FromBody] EditCustomerCommand model) 
            => await this.sender.Send(model);
    }
}