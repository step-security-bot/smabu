using LIT.Smabu.Domain.CustomerAggregate;
using LIT.Smabu.Domain.Exceptions;
using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.Domain.OfferAggregate;
using LIT.Smabu.Shared.Interfaces;

namespace LIT.Smabu.UseCases.Customers.Delete
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
            var hasOffers = (await aggregateStore.GetAllAsync<Offer>()).Where(x => x.CustomerId == id).Any();
            if (hasOffers)
            {
                throw new DomainException("Es sind noch Angebote verknüpft.");
            }
        }

        private async Task CheckInvoices(CustomerId id)
        {
            var hasInvoices = (await aggregateStore.GetAllAsync<Invoice>()).Where(x => x.CustomerId == id).Any();
            if (hasInvoices)
            {
                throw new DomainException("Es sind noch Rechnungen verknüpft.");
            }
        }
    }
}
