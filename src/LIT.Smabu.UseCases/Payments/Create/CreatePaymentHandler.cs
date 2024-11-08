using LIT.Smabu.Domain.PaymentAggregate;
using LIT.Smabu.Domain.Shared;
using LIT.Smabu.Shared;
using LIT.Smabu.UseCases.Shared;
namespace LIT.Smabu.UseCases.Payments.Create
{
    public class CreatePaymentHandler(IAggregateStore store) : ICommandHandler<CreatePaymentCommand, PaymentId>
    {

        public async Task<Result<PaymentId>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            if (!request.Validate())
            {
                return PaymentErrors.InvalidCreate;
            }

            var payment = request.Direction switch
            {
                var direction when direction == PaymentDirection.Incoming 
                    => Payment.CreateIncoming(request.Id, request.Details, request.Payer, request.Payee, 
                        request.CustomerId!, request.InvoiceId!, request.ReferenceNr, request.ReferenceDate, request.AccountingDate, request.AmountDue),
                var direction when direction == PaymentDirection.Outgoing 
                    => Payment.CreateOutgoing(request.Id, request.Details, request.Payer, request.Payee, 
                        request.ReferenceNr, request.ReferenceDate, request.AccountingDate, request.AmountDue),
                _ => throw new InvalidOperationException($"Unknown payment direction: {request.Direction}")
            };

            await store.CreateAsync(payment);
            return payment.Id;
        }
    }
}
