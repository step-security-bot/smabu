using LIT.Smabu.Infrastructure.DDD;
using MediatR;

namespace LIT.Smabu.Infrastructure.CQRS
{
    public abstract class RequestHandler<TQuery, TResponse> : IRequestHandler<TQuery, TResponse> where TQuery : IRequest<TResponse>
    {
        protected RequestHandler(IAggregateStore aggregateStore)
        {
            AggregateStore = aggregateStore;
        }
        protected IAggregateStore AggregateStore { get; }

        public abstract Task<TResponse> Handle(TQuery request, CancellationToken cancellationToken);
    }
}
