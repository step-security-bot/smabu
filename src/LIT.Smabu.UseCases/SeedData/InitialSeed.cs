using LIT.Smabu.Domain.TermsOfPaymentAggregate;
using LIT.Smabu.Shared;

namespace LIT.Smabu.UseCases.SeedData
{
    public class InitialSeed(IAggregateStore aggregateStore)
    {

        public async Task StartAsync()
        {
            await SeedTermsOfPaymentsAsync();
        }

        private async Task SeedTermsOfPaymentsAsync()
        {
            var items = await aggregateStore.GetAllAsync<TermsOfPayment>();
            if (!items.Any())
            {
                var item1 = TermsOfPayment.Create(new(Guid.Parse("89E12676-A882-4B7C-B18F-F8C3D9A484E8")), "15 Tage Netto", "Zahlbar innerhalb von 15 Tagen ohne Abzüge.", 15);
                await aggregateStore.CreateAsync(item1);

                var item2 = TermsOfPayment.Create(new(Guid.Parse("DB9DA680-98E5-4AD0-A25A-20FBD6395449")), "Vorkasse", "Zahlbar sofort.", null);
                await aggregateStore.CreateAsync(item2);
            }
        }
    }
}
