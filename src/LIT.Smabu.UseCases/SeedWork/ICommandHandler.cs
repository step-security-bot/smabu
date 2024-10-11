﻿using LIT.Smabu.Domain.SeedWork;
using MediatR;
using System.Windows.Input;

namespace LIT.Smabu.UseCases.SeedWork
{
    public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
        where TCommand : ICommand<TResponse>
    {

    }

    public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
        where TCommand : IRequest<Result>
    {

    }
}
