using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.UseCases.Offers.AddOfferItem;
using LIT.Smabu.UseCases.Offers.Create;
using LIT.Smabu.UseCases.Offers.Delete;
using LIT.Smabu.UseCases.Offers.Get;
using LIT.Smabu.UseCases.Offers.List;
using LIT.Smabu.UseCases.Offers.RemoveOfferItem;
using LIT.Smabu.UseCases.Offers.Update;
using LIT.Smabu.UseCases.Offers.UpdateOfferItem;
using MediatR;
using LIT.Smabu.Domain.OfferAggregate;
using LIT.Smabu.UseCases.Offers;
using LIT.Smabu.Domain.SeedWork;

namespace LIT.Smabu.API.Endpoints
{
    public static class Offers
    {
        public static void RegisterOffersEndpoints(this IEndpointRouteBuilder routes)
        {
            var api = routes.MapGroup("/offers")
                .WithTags(["Offers"])
                .RequireAuthorization();

            RegisterOffer(api);
            RegisterOfferItem(api);
        }

        private static void RegisterOffer(RouteGroupBuilder api)
        {
            api.MapPost("/", async (IMediator mediator, CreateOfferCommand command) =>
                await mediator.SendAndMatchAsync(command,
                    onSuccess: Results.Ok,
                    onFailure: Results.BadRequest))
                .Produces<OfferId>();

            api.MapGet("/", async (IMediator mediator) =>
                await mediator.SendAndMatchAsync(new ListOffersQuery(),
                    onSuccess: Results.Ok,
                    onFailure: Results.BadRequest))
                .Produces<OfferDTO[]>();

            api.MapGet("/{id}", async (IMediator mediator, Guid id, bool withItems = false) =>
                await mediator.SendAndMatchAsync(new GetOfferQuery(new(id)) { WithItems = withItems },
                    onSuccess: Results.Ok,
                    onFailure: Results.BadRequest))
                .Produces<OfferDTO[]>();

            api.MapPut("/{id}", async (IMediator mediator, Guid id, UpdateOfferCommand command) =>
                await mediator.SendAndMatchAsync(command,
                    onSuccess: Results.Ok,
                    onFailure: Results.BadRequest))
                .Produces<OfferId>()
                .Produces<Error>(400);

            api.MapDelete("/{id}", async (IMediator mediator, Guid id) =>
                await mediator.SendAndMatchAsync(new DeleteOfferCommand(new(id)),
                    onSuccess: () => Results.Ok(),
                    onFailure: Results.BadRequest))
                .Produces(200)
                .Produces<Error>(400);
        }

        private static void RegisterOfferItem(RouteGroupBuilder api)
        {
            api.MapPost("/{id}/items", async (IMediator mediator, Guid id, AddOfferItemCommand command) =>
                await mediator.SendAndMatchAsync(command,
                    onSuccess: Results.Ok,
                    onFailure: Results.BadRequest))
                .Produces<InvoiceItemId>();

            api.MapPut("/{id}/items/{itemId}", async (IMediator mediator, Guid id, Guid itemId, UpdateOfferItemCommand command) =>
                await mediator.SendAndMatchAsync(command,
                    onSuccess: () => Results.Ok(),
                    onFailure: Results.BadRequest))
                .Produces<InvoiceItemId>();

            //api.MapPut("/{id}/items/{itemId}/movedown", async (IMediator mediator, Guid id, Guid itemId) =>
            //    await mediator.SendAndMatchAsync(new MoveOfferItemDownCommand(new(itemId), new(id)),
            //        onSuccess: () => Results.Ok(),
            //        onFailure: Results.BadRequest))
            //    .Produces(200)
            //    .Produces<Error>(400);

            //api.MapPut("/{id}/items/{itemId}/moveup", async (IMediator mediator, Guid id, Guid itemId) =>
            //    await mediator.SendAndMatchAsync(new MoveOfferItemUpCommand(new(itemId), new(id)),
            //        onSuccess: () => Results.Ok(),
            //        onFailure: Results.BadRequest))
            //    .Produces(200)
            //    .Produces<Error>(400);

            api.MapDelete("/{id}/items/{itemId}", async (IMediator mediator, Guid id, Guid itemId) =>
                await mediator.SendAndMatchAsync(new RemoveOfferItemCommand(new(itemId), new(id)),
                    onSuccess: () => Results.Ok(),
                    onFailure: Results.BadRequest))
                .Produces(200)
                .Produces<Error>(400);
        }
    }
}
