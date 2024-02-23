using MediatR;

namespace LIT.Smabu.Shared.Interfaces
{
    public interface ICommand<T> : IRequest<T>
    {
    }
}
