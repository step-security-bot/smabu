using LIT.Smabu.Domain.Services;
using LIT.Smabu.UseCases.SeedWork;

namespace LIT.Smabu.UseCases.Customers.Delete
{
    public class DeleteCustomerHandler(DeleteCustomerService deleteCustomerService) : ICommandHandler<DeleteCustomerCommand, bool>
    {
        public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            await deleteCustomerService.DeleteAsync(request.Id);
            return true;
        }
    }
}
