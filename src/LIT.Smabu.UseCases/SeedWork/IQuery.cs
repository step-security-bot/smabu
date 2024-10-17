using LIT.Smabu.Shared;
using MediatR;

namespace LIT.Smabu.UseCases.SeedWork
{
    public interface IQuery<T> : IRequest<Result<T>>
    {
    }
}
