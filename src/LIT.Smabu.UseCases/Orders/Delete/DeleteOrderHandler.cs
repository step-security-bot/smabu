using LIT.Smabu.Domain.Shared;
using LIT.Smabu.Shared;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Orders.Delete
{
    public class DeleteOrderHandler(IAggregateStore store) : ICommandHandler<DeleteOrderCommand>
    {
        public async Task<Result> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await store.GetByAsync(request.Id);
            order.Delete();
            await store.DeleteAsync(order);
            return Result.Success();
        }
    }
}
