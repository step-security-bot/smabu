using LIT.Smabu.Domain.PaymentAggregate;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Payments.Get
{
    public record GetPaymentQuery(PaymentId PaymentId) : IQuery<PaymentDTO>
    {

    }
}
