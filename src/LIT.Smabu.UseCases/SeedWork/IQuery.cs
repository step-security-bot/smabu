using LIT.Smabu.Domain.SeedWork;
using MediatR;

namespace LIT.Smabu.UseCases.SeedWork
{
    public interface IQuery<T> : IRequest<Result<T>>
    {
    }
}
