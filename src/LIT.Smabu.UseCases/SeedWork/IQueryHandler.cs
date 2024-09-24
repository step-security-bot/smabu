﻿using LIT.Smabu.Domain.SeedWork;
using MediatR;

namespace LIT.Smabu.UseCases.SeedWork
{
    public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
        where TQuery : IQuery<TResponse>
    {
    }
}
