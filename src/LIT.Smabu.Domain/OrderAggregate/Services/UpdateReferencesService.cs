using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.Domain.OfferAggregate;
using LIT.Smabu.Domain.OrderAggregate.Specifications;
using LIT.Smabu.Domain.Shared;
using LIT.Smabu.Shared;

namespace LIT.Smabu.Domain.OrderAggregate.Services
{
    public class UpdateReferencesService(IAggregateStore store)
    {
        public async Task<Result> StartAsync(OrderId orderId, OrderReferences references)
        {
            var order = await store.GetByAsync(orderId);

            if (order == null)
            {
                return Result.Failure(OrderErrors.NotFound);
            }

            var checkResult = await CheckReferencesAsync(references);
            if (checkResult.IsSuccess)
            {
                order.UpdateReferences(references);
                await store.UpdateAsync(order);
            }
            return checkResult;
        }

        private async Task<Result> CheckReferencesAsync(OrderReferences references)
        {
            var errors = new List<Error>();
            foreach (var entityId in references.AllReferenceIds)
            {
                var detectedOrder = (await store.ApplySpecification(new DetectOrderForReferenceIdSpec(entityId))).SingleOrDefault();
                if (detectedOrder != null)
                {
                    errors.Add(OrderErrors.ReferenceAlreadyAdded(entityId, detectedOrder.Number));
                }
            }
            if (errors.Count != 0)
            {
                return Result.Failure(errors);
            }
            else
            {
                return Result.Success();
            }
        }
    }
}
