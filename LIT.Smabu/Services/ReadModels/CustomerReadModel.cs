using LIT.Smabu.Infrastructure.CQRS;
using LIT.Smabu.Infrastructure.DDD;
using LIT.Smabu.Infrastructure.Exception;
using LIT.Smabu.Shared.Dtos;
using LIT.Smabu.Shared.Entities.Business.CustomerAggregate;

namespace LIT.Smabu.Service.ReadModels
{
    public class CustomerReadModel : EntityReadModel<Customer, CustomerId>
    {
        public CustomerReadModel(IAggregateStore aggregateStore) : base(aggregateStore)
        {

        }

        protected override IEnumerable<Customer> BuildQuery(IAggregateStore aggregateStore)
        {
            return aggregateStore.GetAll<Customer, CustomerId>().OrderBy(x => x.Number);
        }

        public IEnumerable<CustomerOverviewDto> GetOverview() => GetAll().Select(x => CustomerOverviewDto.From(x));
        public CustomerDetailDto GetDetail(CustomerId id) => CustomerDetailDto.From(GetById(id) ?? throw new EntityNotFoundException(id));
    }
}
