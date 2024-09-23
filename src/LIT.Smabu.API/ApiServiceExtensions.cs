using LIT.Smabu.Domain.SeedWork;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace LIT.Smabu.API
{
    public static class ApiServiceExtensions
    {
        public static IResult Match<T>(this Result<T> result)
        {
            return result.IsSuccess
                    ? Results.Ok(result.Value)
                    : Results.BadRequest(result.Error);
        }

        public static IResult Match(this Result result)
        {
            return result.IsSuccess
                    ? Results.Ok()
                    : Results.BadRequest(result.Error);
        }

        public static async Task<IResult> SendAndMatchAsync<TCommand>(this IMediator mediator, TCommand command)
        {
            var response = await mediator.Send(command!);
            if (response is Result result)
            {
                return result.Match();
            }
            else
            {
                return Results.Ok(response);
            }
        }
    }
}
