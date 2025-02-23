﻿using LIT.Smabu.Domain.Shared;
using LIT.Smabu.Shared;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.TermsOfPayments.Get
{
    public class GetTermsOfPaymentHandler(IAggregateStore store) : IQueryHandler<GetTermsOfPaymentQuery, TermsOfPaymentDTO>
    {
        public async Task<Result<TermsOfPaymentDTO>> Handle(GetTermsOfPaymentQuery request, CancellationToken cancellationToken)
        {
            var termsOfPayment = await store.GetByAsync(request.Id);
            return TermsOfPaymentDTO.CreateFrom(termsOfPayment);
        }
    }
}
