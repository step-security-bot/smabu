using MediatR;
using LIT.Smabu.UseCases.Catalogs.Get;
using LIT.Smabu.UseCases.Catalogs;
using LIT.Smabu.Domain.CustomerAggregate;
using LIT.Smabu.UseCases.Catalogs.GetItem;
using LIT.Smabu.UseCases.Catalogs.UpdateItem;
using LIT.Smabu.UseCases.Catalogs.Update;
using LIT.Smabu.UseCases.Catalogs.Delete;
using LIT.Smabu.Domain.Shared;
using LIT.Smabu.UseCases.Catalogs.RemoveItem;
using LIT.Smabu.UseCases.Catalogs.GetGroup;
using LIT.Smabu.UseCases.Catalogs.UpdateGroup;
using LIT.Smabu.UseCases.Catalogs.RemoveGroup;

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
            MapCatalogGroups(api);
            MapCatalogItems(api);
        }

        private static void MapCatalogs(RouteGroupBuilder api)
        {
            api.MapGet("/default", async (IMediator mediator) =>
                await mediator.SendAndMatchAsync(new GetCatalogQuery(),
                    onSuccess: Results.Ok,
                    onFailure: Results.BadRequest))
                .Produces<CatalogDTO>();

            api.MapPut("/{id}", async (IMediator mediator, Guid catalogId, Guid id, UpdateCatalogCommand command) =>
                await mediator.SendAndMatchAsync(command,
                    onSuccess: () => Results.Ok(),
                    onFailure: Results.BadRequest))
                .Produces<CustomerId>();

            api.MapDelete("/{id}", async (IMediator mediator, Guid id) =>
                await mediator.SendAndMatchAsync(new DeleteCatalogCommand(new(id)),
                    onSuccess: () => Results.Ok(),
                    onFailure: Results.BadRequest))
                .Produces(200)
                .Produces<Error>(400);
        }

        private static void MapCatalogGroups(RouteGroupBuilder api)
        {
            api.MapGet("/{catalogId}/groups/{id}", async (IMediator mediator, Guid catalogId, Guid id) =>
                await mediator.SendAndMatchAsync(new GetCatalogGroupQuery(new(id), new(catalogId)),
                    onSuccess: Results.Ok,
                    onFailure: Results.BadRequest))
                .Produces<CatalogDTO>();

            api.MapPut("/{catalogId}/groups/{id}", async (IMediator mediator, Guid catalogId, Guid id, UpdateCatalogGroupCommand command) =>
                await mediator.SendAndMatchAsync(command,
                    onSuccess: () => Results.Ok(),
                    onFailure: Results.BadRequest))
                .Produces<CustomerId>();

            api.MapDelete("/{catalogId}/groups/{id}", async (IMediator mediator, Guid catalogId, Guid id) =>
                await mediator.SendAndMatchAsync(new RemoveCatalogGroupCommand(new(catalogId), new(id)),
                    onSuccess: () => Results.Ok(),
                    onFailure: Results.BadRequest))
                .Produces(200)
                .Produces<Error>(400);
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

            api.MapDelete("/{catalogId}/items/{id}", async (IMediator mediator, Guid catalogId, Guid id) =>
                await mediator.SendAndMatchAsync(new RemoveCatalogItemCommand(new(catalogId), new(id)),
                    onSuccess: () => Results.Ok(),
                    onFailure: Results.BadRequest))
                .Produces(200)
                .Produces<Error>(400);
        }
    }
}
