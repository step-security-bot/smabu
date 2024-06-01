using MediatR;

namespace LIT.Smabu.UseCases.SeedWork
{
    public interface ICommand<T> : IRequest<T>
    {
    }
}
