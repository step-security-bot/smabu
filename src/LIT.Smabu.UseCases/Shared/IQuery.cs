using LIT.Smabu.Domain.Shared;
using LIT.Smabu.Shared;
using MediatR;

namespace LIT.Smabu.UseCases.Shared
{
    public interface IQuery<T> : IRequest<Result<T>>
    {
    }
}
