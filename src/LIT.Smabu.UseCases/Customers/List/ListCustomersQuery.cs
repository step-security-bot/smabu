using LIT.Smabu.Shared.Interfaces;

namespace LIT.Smabu.UseCases.Customers.List
{
    public record ListCustomersQuery : IQuery<CustomerDTO[]>
    {
    }
}
