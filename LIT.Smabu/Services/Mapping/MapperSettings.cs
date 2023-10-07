using LIT.Smabu.Business.Service.Customers.Common;
using LIT.Smabu.Domain.Shared.Contracts;
using LIT.Smabu.Infrastructure.Shared.Contracts;
using LIT.Smabu.Shared.Customers;

namespace LIT.Smabu.Business.Service.Mapping
{
    public class MapperSettings : IMapperSettings
    {
        private readonly IAggregateStore aggregateStore;
        private readonly IAggregateResolver aggregateResolver;

        public MapperSettings(IAggregateStore aggregateStore, IAggregateResolver aggregateResolver)
        {
            this.aggregateStore = aggregateStore;
            this.aggregateResolver = aggregateResolver;
        }

        public async  Task PostHandleAsync<TDest>(TDest dest)
        {
            if (dest is CustomerDTO customerDTO)
            {
                var customerTotalSales = new CustomerTotalSalesCalculator(aggregateStore);
                await customerTotalSales.StartAsync(customerDTO.Id);
                customerDTO.TotalSales = customerTotalSales.TotalSales;
            }
        }

        public async Task<Dictionary<IEntityId, IAggregateRoot>> ResolveAggregatesAsync(IEntityId[] entityIds)
        {
            return await this.aggregateResolver.ResolveByIdsAsync(entityIds);
        }
    }
}
