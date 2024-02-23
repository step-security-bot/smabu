using LIT.Smabu.Shared.Interfaces;
using LIT.Smabu.UseCases.Offers;

namespace LIT.Smabu.UseCases.Offers.List
{
    public record ListOffersQuery : IQuery<OfferDTO[]>
    {
    }
}
