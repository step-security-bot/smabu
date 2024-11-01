using LIT.Smabu.Domain.CustomerAggregate;
using LIT.Smabu.UseCases.Customers.Create;
using LIT.Smabu.UseCases.Customers;
using MediatR;
using LIT.Smabu.UseCases.Catalogs.Get;
using LIT.Smabu.UseCases.Catalogs;

namespace LIT.Smabu.API.Endpoints
{
    public static class Catalogs
    {
        public static void RegisterCatalogsEndpoints(this IEndpointRouteBuilder routes)
        {
            var api = routes.MapGroup("/catalogs")
                .WithTags(["Catalogs"])
                .RequireAuthorization();

            api.MapGet("/default", async (IMediator mediator) =>
                await mediator.SendAndMatchAsync(new GetCatalogQuery(),
                    onSuccess: Results.Ok,
                    onFailure: Results.BadRequest))
                .Produces<CatalogDTO>();
        }
    }
}
