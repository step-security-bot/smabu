using LIT.Smabu.Infrastructure.CQRS;
using LIT.Smabu.Infrastructure.DDD;
using LIT.Smabu.Infrastructure.Exception;
using LIT.Smabu.Shared.Domain.Customers;
using LIT.Smabu.Shared.Domain.Customers.Queries;

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

        public IEnumerable<GetAllCustomersResponse> GetOverview() => GetAll().Select(x => GetAllCustomersResponse.From(x));
        public GetCustomerDetailResponse GetDetail(CustomerId id) => GetCustomerDetailResponse.From(GetById(id) ?? throw new EntityNotFoundException(id));
    }
}
