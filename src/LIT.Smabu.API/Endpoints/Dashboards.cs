using MediatR;
using LIT.Smabu.UseCases.Dashboards.Welcome;

namespace LIT.Smabu.API.Endpoints
{
    public static class Dashboards
    {
        public static void RegisterDashboardEndpoints(this IEndpointRouteBuilder routes)
        {
            var api = routes.MapGroup("/dashboards")
            .WithTags(["Dashboards"])
            .RequireAuthorization();

            api.MapGet("/welcome", async (IMediator mediator) =>
                await mediator.SendAndMatchAsync(new GetWelcomeDashboardQuery(),
                    onSuccess: Results.Ok,
                    onFailure: Results.BadRequest))
                .Produces<GetWelcomeDashboardReadModel>();
        }

    }
}
