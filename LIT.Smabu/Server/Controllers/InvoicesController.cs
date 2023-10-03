using LIT.Smabu.Service.ReadModels;
using LIT.Smabu.Shared.Dtos;
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
        private readonly ILogger<InvoicesController> _logger;
        private readonly InvoiceReadModel invoiceReadModel;

        public InvoicesController(ILogger<InvoicesController> logger, InvoiceReadModel invoiceReadModel)
        {
            _logger = logger;
            this.invoiceReadModel = invoiceReadModel;
        }

        [HttpGet("")]
        public IEnumerable<InvoiceOverviewDto> Get()
        {
            return this.invoiceReadModel.GetOverview();
        }

        //[HttpGet("{id}")]
        //public CustomerDetailDto GetDetails(Guid id)
        //{
        //    return this.customerReadModel.GetDetail(new CustomerId(id));
        //}

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