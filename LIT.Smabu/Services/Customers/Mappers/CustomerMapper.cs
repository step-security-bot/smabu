using LIT.Smabu.Business.Service.Common.Mappers;
using LIT.Smabu.Business.Service.Contratcs;
using LIT.Smabu.Business.Service.Customers.Common;
using LIT.Smabu.Domain.Shared.Contracts;
using LIT.Smabu.Domain.Shared.Customers;
using LIT.Smabu.Infrastructure.Shared.Contracts;
using LIT.Smabu.Shared.Customers;

namespace LIT.Smabu.Business.Service.Customers.Mappers
{
    public class CustomerMapper : IMapperManyAsync<Customer, CustomerDTO>
    {
        private readonly IAggregateStore aggregateStore;

        public CustomerMapper(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

        public async Task<Dictionary<IEntityId, CustomerDTO>> MapAsync(IEnumerable<Customer> source)
        {
            var result = new Dictionary<IEntityId, CustomerDTO>();
            foreach (var item in source)
            {
                result.Add(item.Id, await MapAsync(item));
            }
            return result;
        }

        public async Task<CustomerDTO> MapAsync(Customer source)
        {
            var totalSales = new CustomerTotalSalesCalculator(aggregateStore);
            await totalSales.StartAsync(source.Id);
            return new CustomerDTO
            {
                Id = source.Id,
                Number = source.Number,
                Name = source.Name,
                IndustryBranch = source.IndustryBranch,
                Currency = source.Currency,
                MainAddress = new AddressMapper().Map(source.MainAddress),
                Communication = new CommunicationMapper().Map(source.Communication),
                TotalSales = totalSales.TotalSales
            };
        }
    }
}
