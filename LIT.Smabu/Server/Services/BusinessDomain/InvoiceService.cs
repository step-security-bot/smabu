using LIT.Smabu.Infrastructure.DDD;
using LIT.Smabu.Shared.BusinessDomain.Customer;
using LIT.Smabu.Shared.BusinessDomain.Invoice;

namespace LIT.Smabu.Server.Services.Business
{
    public class InvoiceService
    {
        private readonly IAggregateRepository aggregateRepository;
        private readonly IEntityRepositoryFactory entityRepositoryFactory;

        public InvoiceService(IAggregateRepository aggregateRepository, IEntityRepositoryFactory entityRepositoryFactory)
        {
            this.aggregateRepository = aggregateRepository;
            this.entityRepositoryFactory = entityRepositoryFactory;
        }

        public async Task<IInvoice> CreateAsync()
        {
            var invoice = Invoice.Create(new InvoiceId(Guid.NewGuid()), "?", new Shared.BusinessDomain.Period(DateTime.Now, DateTime.Now), new CustomerId(Guid.NewGuid()), 0, "");
            await this.aggregateRepository.AddOrUpdateAsync(invoice);
            return invoice;
        }
    }
}
