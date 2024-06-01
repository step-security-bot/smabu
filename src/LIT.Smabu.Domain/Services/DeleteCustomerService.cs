using LIT.Smabu.Domain.CustomerAggregate;
using LIT.Smabu.Domain.Exceptions;
using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.Domain.OfferAggregate.Specifications;
using LIT.Smabu.Domain.SeedWork;

namespace LIT.Smabu.Domain.Services
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
            var hasOffers = (await aggregateStore.ApplySpecification(new OffersByCustomerIdSpec(id))).Any();
            if (hasOffers)
            {
                throw new DomainException("Es sind bereits Angebote verknüpft.", id);
            }
        }

        private async Task CheckInvoices(CustomerId id)
        {
            var hasInvoices = (await aggregateStore.ApplySpecification(new InvoicesByCustomerIdSpec(id))).Any();
            if (hasInvoices)
            {
                throw new DomainException("Es sind bereits Rechnungen verknüpft.", id);
            }
        }
    }
}
