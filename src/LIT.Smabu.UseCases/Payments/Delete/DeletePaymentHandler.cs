﻿using LIT.Smabu.Domain.PaymentAggregate;
using LIT.Smabu.Domain.Shared;
using LIT.Smabu.Shared;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Payments.Delete
{
    public class DeletePaymentHandler(IAggregateStore store) : ICommandHandler<DeletePaymentCommand>
    {
        public async Task<Result> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = await store.GetByAsync(request.Id);
            if (payment == null)
            {
                return PaymentErrors.NotFound;
            }
            var deleteResult = payment.Delete();
            if (deleteResult.IsSuccess) {
                await store.DeleteAsync(payment);
            }
            return deleteResult;
        }
    }
}
