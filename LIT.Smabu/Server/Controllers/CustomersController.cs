using LIT.Smabu.Shared.Domain.Customers;
using LIT.Smabu.Shared.Domain.Customers.Commands;
using LIT.Smabu.Shared.Domain.Customers.Queries;
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
        private readonly ILogger<CustomersController> _logger;
        private readonly ISender sender;

        public CustomersController(ILogger<CustomersController> logger, ISender sender)
        {
            _logger = logger;
            this.sender = sender;
        }

        [HttpGet("")]
        public async Task<GetAllCustomersResponse[]> Get()
        {
            var result = await this.sender.Send(new GetAllCustomersQuery());
            return result;
        }

        [HttpGet("{id}")]
        public async Task<GetCustomerDetailsResponse> GetDetails(Guid id)
        {
            var result = await this.sender.Send(new GetCustomerDetailsQuery(new CustomerId(id)));
            return result;
        }

        [HttpPost]
        public async Task<CustomerId> Post([FromBody] CreateCustomerCommand model)
        {
            var result = await this.sender.Send(model);
            return result;
        }

        [HttpPut("{id}")]
        public async Task<CustomerId> Put([FromBody] EditCustomerCommand model)
        {
            var result = await this.sender.Send(model);
            return result;
        }
    }
}