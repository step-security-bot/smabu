using LIT.Smabu.Service.Business;
using LIT.Smabu.Service.ReadModels;
using LIT.Smabu.Shared.Domain.Customers;
using LIT.Smabu.Shared.Domain.Customers.Commands;
using LIT.Smabu.Shared.Domain.Customers.Queries;
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
        private readonly CustomerService customerService;
        private readonly CustomerReadModel customerReadModel;

        public CustomersController(ILogger<CustomersController> logger, CustomerService customerService, CustomerReadModel customerReadModel)
        {
            _logger = logger;
            this.customerService = customerService;
            this.customerReadModel = customerReadModel;
        }

        [HttpGet("")]
        public IEnumerable<GetAllCustomersResponse> Get()
        {
            return this.customerReadModel.GetOverview();
        }

        [HttpGet("{id}")]
        public GetCustomerDetailResponse GetDetails(Guid id)
        {
            return this.customerReadModel.GetDetail(new CustomerId(id));
        }

        [HttpPost]
        public async Task<GetAllCustomersResponse> Post([FromBody] CreateCustomerCommand model)
        {
            var customer = await this.customerService.CreateAsync(model.Name);
            return GetAllCustomersResponse.From(customer);
        }

        [HttpPut]
        public async Task<GetAllCustomersResponse> Put([FromBody] EditCustomerCommand model)
        {
            var customer = await this.customerService.EditAsync(model.Id, model.Name, model.IndustryBranch ?? "");
            return GetAllCustomersResponse.From(customer);
        }
    }
}