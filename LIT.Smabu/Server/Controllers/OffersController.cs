using LIT.Smabu.Business.Service.Offers.Commands;
using LIT.Smabu.Business.Service.Offers.Queries;
using LIT.Smabu.Domain.Shared.Invoices;
using LIT.Smabu.Domain.Shared.Offers;
using LIT.Smabu.Shared.Offers;
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
    public class OffersController : ControllerBase
    {
        private readonly ISender sender;

        public OffersController(ISender sender)
        {
            this.sender = sender;
        }

        [HttpGet("")]
        public async Task<OfferDTO[]> Get()
            => await this.sender.Send(new GetOffersQuery());

        [HttpGet("{id}")]
        public async Task<OfferDTO> GetDetails(Guid id)
            => await this.sender.Send(new GetOfferByIdQuery(new OfferId(id)));

        [HttpPost]
        public async Task<OfferDTO> Post([FromBody] CreateOfferDTO model)
            => await this.sender.Send(new CreateOfferCommand
            {
                Id = model.Id,
                CustomerId = model.CustomerId,
                Currency = model.Currency,
            });

        [HttpPut("{id}")]
        public async Task<OfferDTO> Put([FromBody] EditOfferDTO model)
            => await this.sender.Send(new EditOfferCommand
            {
                Id = model.Id,
                OfferDate = DateOnly.FromDateTime(model.OfferDate),
                ExpiresOn = DateOnly.FromDateTime(model.ExpiresOn),
                Tax = model.Tax,
                TaxDetails = model.TaxDetails,                
            });

        [HttpPost("{id}/items")]
        public async Task<OfferItemDTO> PostOfferItem([FromBody] AddOfferItemDTO model)
            => await this.sender.Send(new AddOfferItemCommand
            {
                Id = model.Id,
                OfferId = model.OfferId,
                Details = model.Details,
                Quantity = model.Quantity,
                UnitPrice = model.UnitPrice
            });

        [HttpPut("{id}/items/{itemId}")]
        public async Task<OfferItemDTO> PutOfferLine([FromBody] EditOfferItemDTO model)
            => await this.sender.Send(new EditOfferItemCommand
            {
                Id = model.Id,
                OfferId = model.OfferId,
                Details = model.Details,
                Quantity = model.Quantity,
                UnitPrice = model.UnitPrice
            });

        [HttpDelete("{id}/items/{itemId}")]
        public async Task PutOfferLine(Guid itemId, Guid id)
            => await this.sender.Send(new RemoveOfferItemCommand
            {
                Id = new OfferItemId(itemId),
                OfferId = new OfferId(id)
            });
    }
}