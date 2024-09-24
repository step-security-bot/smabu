using LIT.Smabu.Domain.SeedWork;
using MediatR;

namespace LIT.Smabu.API
{
    public static class ApiServiceExtensions
    {
        public static IResult Match(this Result result)
        {
            if (result.IsSuccess)
            {
                if (result.HasReturnValue)
                {
                    return Results.Ok(result.GetValue());
                }
                else
                {
                    return Results.Ok();
                }
            }
            else
            {
                return Results.BadRequest(result.Error);
            }
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
