using LIT.Smabu.Domain.Shared.Common;
using LIT.Smabu.Domain.Shared.Invoices;
using LIT.Smabu.Domain.Shared.Offers;
using LIT.Smabu.Shared.Offers;
using MediatR;

namespace LIT.Smabu.Business.Service.Offers.Commands
{
    public record AddOfferItemCommand : IRequest<OfferItemDTO>
    {
        public required OfferItemId Id { get; set; }
        public required OfferId OfferId { get; set; }
        public required string Details { get; set; }
        public required Quantity Quantity { get; set; }
        public required decimal UnitPrice { get; set; }
    }
}
