using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.Domain.OfferAggregate;
using LIT.Smabu.Domain.OrderAggregate;
using LIT.Smabu.Shared.Interfaces;
using MediatR;

namespace LIT.Smabu.UseCases.Customers.Delete
{
    public class DeleteCustomerHandler(DeleteCustomerService deleteCustomerService) : IRequestHandler<DeleteCustomerCommand, bool>
    {
        private readonly DeleteCustomerService deleteCustomerService = deleteCustomerService;

        public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            await deleteCustomerService.DeleteAsync(request.Id);
            return true;
        }
    }
}
