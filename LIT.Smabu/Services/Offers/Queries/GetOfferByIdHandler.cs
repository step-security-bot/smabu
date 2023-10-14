using LIT.Smabu.Business.Service.Invoices.Mappings;
using LIT.Smabu.Business.Service.Offers.Mappers;
using LIT.Smabu.Domain.Shared.Invoices;
using LIT.Smabu.Infrastructure.Shared.Contracts;
using LIT.Smabu.Shared.Offers;
using MediatR;

namespace LIT.Smabu.Business.Service.Offers.Queries
{
    public class GetOfferByIdHandler : IRequestHandler<GetOfferByIdQuery, OfferDTO>
    {
        private readonly IAggregateStore aggregateStore;

        public GetOfferByIdHandler(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

        public async Task<OfferDTO> Handle(GetOfferByIdQuery request, CancellationToken cancellationToken)
        {
            var offer = await aggregateStore.GetByAsync(request.Id);
            var result = await new OfferMapper(aggregateStore).MapAsync(offer);
            return result;
        }
    }
}
