using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Payments.List
{
    public record ListPaymentsQuery : IQuery<PaymentDto[]>
    {
    }
}
