using LIT.Smabu.Domain.Errors;
using LIT.Smabu.Domain.InvoiceAggregate.Specifications;
using LIT.Smabu.Domain.OfferAggregate.Specifications;
using LIT.Smabu.Domain.SeedWork;

namespace LIT.Smabu.Domain.CustomerAggregate.Services
{
    public class DeleteCustomerService(IAggregateStore aggregateStore)
    {
        public async Task DeleteAsync(CustomerId id)
        {
            await CheckOffers(id);
            await CheckInvoices(id);

            var customer = await aggregateStore.GetByAsync(id);
            customer.Delete();
            await aggregateStore.DeleteAsync(customer);
        }

        private async Task CheckOffers(CustomerId id)
        {
            var offers = await aggregateStore.ApplySpecification(new OffersByCustomerIdSpec(id));
            if (offers.Any())
            {
                throw new DomainError("Es sind bereits Angebote verknüpft.", id);
            }
        }

        private async Task CheckInvoices(CustomerId id)
        {
            var invoices = await aggregateStore.ApplySpecification(new InvoicesByCustomerIdSpec(id));
            if (invoices.Any())
            {
                throw new DomainError("Es sind bereits Rechnungen verknüpft.", id);
            }
        }
    }
}
