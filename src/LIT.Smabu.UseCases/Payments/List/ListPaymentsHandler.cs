using LIT.Smabu.Domain.PaymentAggregate;
using LIT.Smabu.Domain.Shared;
using LIT.Smabu.Shared;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Payments.List
{
    public class ListPaymentsHandler(IAggregateStore store) : IQueryHandler<ListPaymentsQuery, PaymentDto[]>
    {
        public async Task<Result<PaymentDto[]>> Handle(ListPaymentsQuery request, CancellationToken cancellationToken)
        {
            var payments = await store.GetAllAsync<Payment>();
            return payments.Select(p => PaymentDto.Create(p)).ToArray();
        }
    }
}
