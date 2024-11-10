using LIT.Smabu.Domain.PaymentAggregate;
using LIT.Smabu.Domain.Services;
using LIT.Smabu.Domain.Shared;
using LIT.Smabu.Shared;
using LIT.Smabu.UseCases.Shared;
namespace LIT.Smabu.UseCases.Payments.Create
{
    public class CreatePaymentHandler(IAggregateStore store, BusinessNumberService businessNumberService) : ICommandHandler<CreatePaymentCommand, PaymentId>
    {

        public async Task<Result<PaymentId>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            if (!request.Validate())
            {
                return PaymentErrors.InvalidCreate;
            }

            var number = await businessNumberService.CreatePaymentNumberAsync();

            var payment = request.Direction switch
            {
                var direction when direction == PaymentDirection.Incoming 
                    => Payment.CreateIncoming(request.PaymentId, number, request.Details, request.Payer, request.Payee, 
                        request.CustomerId!, request.InvoiceId!, request.ReferenceNr, request.ReferenceDate, request.AccountingDate, request.AmountDue, request.DueDate),
                var direction when direction == PaymentDirection.Outgoing 
                    => Payment.CreateOutgoing(request.PaymentId, number, request.Details, request.Payer, request.Payee, 
                        request.ReferenceNr, request.ReferenceDate, request.AccountingDate, request.AmountDue, request.DueDate),
                _ => throw new InvalidOperationException($"Unknown payment direction: {request.Direction}")
            };

            if (request.MarkAsPaid.GetValueOrDefault())
            {
                var completeResult = payment.Complete(request.AmountDue, DateTime.UtcNow);
                if (completeResult.IsFailure)
                {
                    return completeResult.Error;
                }
            }

            await store.CreateAsync(payment);
            return payment.Id;
        }
    }
}
