using LIT.Smabu.Domain.CustomerAggregate;
using LIT.Smabu.Domain.SeedWork;

namespace LIT.Smabu.Domain.OfferAggregate.Specifications
{
    public class OffersByCustomerIdSpec(CustomerId customerId) 
        : Specification<Offer>(x => x.CustomerId == customerId)
    {

    }
}
