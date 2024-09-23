using LIT.Smabu.Domain.Errors;
using LIT.Smabu.Domain.InvoiceAggregate.Specifications;
using LIT.Smabu.Domain.OfferAggregate.Specifications;
using LIT.Smabu.Domain.SeedWork;

namespace LIT.Smabu.Domain.CustomerAggregate.Services
{
    public class DeleteCustomerService(IAggregateStore aggregateStore)
    {
        public async Task<Result> DeleteAsync(CustomerId id)
        {
            var offersOk = await CheckOffers(id);
            var invoicesOk = await CheckInvoices(id);

            if (!offersOk || !invoicesOk)
            {
                return Result.Failure(Error.HasReferences("Customer.CannotDelete", [!offersOk ? "Angebote" : "", !invoicesOk ? "Rechnungen" : ""]));
            }

            var customer = await aggregateStore.GetByAsync(id);
            customer.Delete();
            await aggregateStore.DeleteAsync(customer);
            return Result.Success();
        }

        private async Task<bool> CheckOffers(CustomerId id)
        {
            var offers = await aggregateStore.ApplySpecification(new OffersByCustomerIdSpec(id));
            return !offers.Any();
        }

        private async Task<bool> CheckInvoices(CustomerId id)
        {
            var invoices = await aggregateStore.ApplySpecification(new InvoicesByCustomerIdSpec(id));
            return !invoices.Any();
        }
    }
}
