using LIT.Smabu.Infrastructure.Shared.Contracts;

namespace LIT.Smabu.Business.Service.Mapping
{
    public class Mapper : IMapper
    {
        private readonly IAggregateStore aggregateStore;

        public Mapper(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }
    }
}
