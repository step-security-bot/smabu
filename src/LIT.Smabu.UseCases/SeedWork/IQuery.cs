using MediatR;

namespace LIT.Smabu.Shared.Interfaces
{
    public interface IQuery<T> : IRequest<T>
    {
    }
}
