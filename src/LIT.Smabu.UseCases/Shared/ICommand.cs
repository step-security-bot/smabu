using LIT.Smabu.Domain.Shared;
using LIT.Smabu.Shared;
using MediatR;

namespace LIT.Smabu.UseCases.Shared
{
    public interface ICommand<T> : IRequest<Result<T>>
    {
    }

    public interface ICommand : IRequest<Result>
    {
    }
}
