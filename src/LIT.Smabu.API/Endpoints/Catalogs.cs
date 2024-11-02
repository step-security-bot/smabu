using MediatR;
using LIT.Smabu.UseCases.Catalogs.Get;
using LIT.Smabu.UseCases.Catalogs;
using LIT.Smabu.UseCases.Catalogs.GetCatalogItem;
using LIT.Smabu.Domain.CustomerAggregate;
using LIT.Smabu.UseCases.Customers.Update;
using LIT.Smabu.UseCases.Catalogs.UpdateCatalogItem;

namespace LIT.Smabu.API.Endpoints
{
    public static class Catalogs
    {
        public static void RegisterCatalogsEndpoints(this IEndpointRouteBuilder routes)
        {
            var api = routes.MapGroup("/catalogs")
                .WithTags(["Catalogs"])
                .RequireAuthorization();

            MapCatalogs(api);

            MapCatalogItems(api);
        }

        private static void MapCatalogs(RouteGroupBuilder api)
        {
            api.MapGet("/default", async (IMediator mediator) =>
                await mediator.SendAndMatchAsync(new GetCatalogQuery(),
                    onSuccess: Results.Ok,
                    onFailure: Results.BadRequest))
                .Produces<CatalogDTO>();
        }

        private static void MapCatalogItems(RouteGroupBuilder api)
        {
            api.MapGet("/{catalogId}/items/{id}", async (IMediator mediator, Guid catalogId, Guid id) =>
                await mediator.SendAndMatchAsync(new GetCatalogItemQuery(new(id), new(catalogId)),
                    onSuccess: Results.Ok,
                    onFailure: Results.BadRequest))
                .Produces<CatalogDTO>();

            api.MapPut("/{catalogId}/items/{id}", async (IMediator mediator, Guid catalogId, Guid id, UpdateCatalogItemCommand command) =>
                await mediator.SendAndMatchAsync(command,
                    onSuccess: () => Results.Ok(),
                    onFailure: Results.BadRequest))
                .Produces<CustomerId>();
        }
    }
}
