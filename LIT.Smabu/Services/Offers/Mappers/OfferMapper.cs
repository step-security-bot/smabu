using LIT.Smabu.Business.Service.Contratcs;
using LIT.Smabu.Business.Service.Invoices.Mappings;
using LIT.Smabu.Domain.Shared.Contracts;
using LIT.Smabu.Domain.Shared.Invoices;
using LIT.Smabu.Infrastructure.Shared.Contracts;
using LIT.Smabu.Shared.Offers;

namespace LIT.Smabu.Business.Service.Offers.Mappers
{
    public class OfferMapper : IMapperManyAsync<Offer, OfferDTO>, IMapper<OfferItem, OfferItemDTO>
    {
        private readonly IAggregateStore aggregateStore;

        public OfferMapper(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

        public async Task<Dictionary<IEntityId, OfferDTO>> MapAsync(IEnumerable<Offer> source)
        {
            var result = new Dictionary<IEntityId, OfferDTO>();
            var customerIds = source.Select(x => x.CustomerId).ToList();
            var customers = await aggregateStore.GetByAsync(customerIds);
            var customerDtos = await new CustomerMapper(aggregateStore).MapAsync(customers.Values);
            foreach (var item in source)
            {
                result.Add(item.Id, new()
                {
                    Id = item.Id,
                    Customer = customerDtos[item.CustomerId],
                    Number = item.Number,
                    Amount = item.Amount,
                    OfferDate = item.OfferDate.ToDateTime(TimeOnly.MinValue),
                    ExpiresOn = item.ExpiresOn.ToDateTime(TimeOnly.MinValue),
                    Currency = item.Currency,
                    Tax = item.Tax,
                    TaxDetails = item.TaxDetails ?? "",
                    Items = item.Items.Select(x => Map(x)).ToList()
                });
            }
            return result;
        }

        public async Task<OfferDTO> MapAsync(Offer source)
        {
            var customer = await aggregateStore.GetByAsync(source.CustomerId);
            var customerDto = await new CustomerMapper(aggregateStore).MapAsync(customer);

            return new()
            {
                Id = source.Id,
                Customer = customerDto,
                Number = source.Number,
                Amount = source.Amount,
                OfferDate = source.OfferDate.ToDateTime(TimeOnly.MinValue),
                ExpiresOn = source.ExpiresOn.ToDateTime(TimeOnly.MinValue),
                Currency = source.Currency,
                Tax = source.Tax,
                TaxDetails = source.TaxDetails ?? "",
                Items = source.Items.Select(x => Map(x)).ToList()
            };
        }

        public OfferItemDTO Map(OfferItem source)
        {
            return new OfferItemDTO()
            {
                Id = source.Id,
                Details = source.Details,
                OfferId = source.OfferId,
                Position = source.Position,
                Quantity = source.Quantity,
                UnitPrice = source.UnitPrice,
                TotalPrice = source.TotalPrice,
                ProductId = source.ProductId,
            };
        }
    }
}
