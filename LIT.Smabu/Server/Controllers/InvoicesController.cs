using LIT.Smabu.Business.Service.Invoices.Commands;
using LIT.Smabu.Business.Service.Invoices.Queries;
using LIT.Smabu.Domain.Shared.Common;
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

        [HttpPost]
        public async Task<InvoiceDTO> Post([FromBody] CreateInvoiceDTO model)
            => await this.sender.Send(new CreateInvoiceCommand
            {
                Id = model.Id,
                CustomerId = model.CustomerId,
                FiscalYear = model.FiscalYear,
                Currency = model.Currency,
                PerformancePeriod = DatePeriod.CreateFrom(DateTime.Now, null)
            });

        [HttpPut("{id}")]
        public async Task<InvoiceDTO> Put([FromBody] InvoiceDTO model)
            => await this.sender.Send(new EditInvoiceCommand {
                Id = model.Id,
                PerformancePeriod = model.PerformancePeriod,
                Tax = model.Tax,
                TaxDetails = model.TaxDetails
            });

        [HttpPut("{id}/lines/{invoiceLineId}")]
        public async Task<InvoiceItemDTO> PutInvoiceLine([FromBody] InvoiceItemDTO model)
            => await this.sender.Send(new EditInvoiceLineCommand {
                Id = model.Id,
                InvoiceId = model.InvoiceId,
                Details = model.Details, 
                Quantity = model.Quantity,
                UnitPrice = model.UnitPrice
            });
    }
}