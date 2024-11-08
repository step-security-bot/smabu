using LIT.Smabu.Domain.PaymentAggregate;
using LIT.Smabu.Domain.Shared;
using LIT.Smabu.Shared;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Payments.Get
{
    public class GetPaymentHandler(IAggregateStore store) : IQueryHandler<GetPaymentQuery, PaymentDto>
    {
        public async Task<Result<PaymentDto>> Handle(GetPaymentQuery request, CancellationToken cancellationToken)
        {
            var payment = await store.GetByAsync(request.PaymentId);
            if (payment == null)
            {
                return PaymentErrors.NotFound;
            }
            return PaymentDto.Create(payment);
        }
    }
}
