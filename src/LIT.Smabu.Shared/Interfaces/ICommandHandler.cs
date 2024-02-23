using MediatR;

namespace LIT.Smabu.Shared.Interfaces
{
    public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    {

    }
}
