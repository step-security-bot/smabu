using LIT.Smabu.Domain.Common;
using LIT.Smabu.Domain.InvoiceAggregate.Specifications;
using LIT.Smabu.Domain.OfferAggregate.Specifications;
using LIT.Smabu.Domain.Shared;

namespace LIT.Smabu.Domain.CustomerAggregate.Services
{
    public class DeleteCustomerService(IAggregateStore aggregateStore)
    {
        public async Task<Result> DeleteAsync(CustomerId id)
        {
            var hasRelations = await CheckHasOffers(id) || await CheckHasInvoices(id);
            if (hasRelations)
            {
                return CommonErrors.HasReferences;
            }

            var customer = await aggregateStore.GetByAsync(id);
            customer.Delete();
            await aggregateStore.DeleteAsync(customer);
            return Result.Success();
        }

        private async Task<bool> CheckHasOffers(CustomerId id)
        {
            var offers = await aggregateStore.ApplySpecification(new OffersByCustomerIdSpec(id));
            return offers.Any();
        }

        private async Task<bool> CheckHasInvoices(CustomerId id)
        {
            var invoices = await aggregateStore.ApplySpecification(new InvoicesByCustomerIdSpec(id));
            return invoices.Any();
        }
    }
}
