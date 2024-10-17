﻿using LIT.Smabu.Domain.Shared;
using MediatR;

namespace LIT.Smabu.UseCases.SeedWork
{
    public interface ICommand<T> : IRequest<Result<T>>
    {
    }

    public interface ICommand : IRequest<Result>
    {
    }
}
