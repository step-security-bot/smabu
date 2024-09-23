using LIT.Smabu.UseCases.Offers.AddOfferItem;
using LIT.Smabu.UseCases.Offers.Create;
using LIT.Smabu.UseCases.Offers.Delete;
using LIT.Smabu.UseCases.Offers.RemoveOfferItem;
using LIT.Smabu.UseCases.Offers.Update;
using LIT.Smabu.UseCases.Offers.UpdateOfferItem;
using MediatR;

namespace LIT.Smabu.API.Endpoints
{
    public static class Offers
    {
        public static void RegisterOffersEndpoints(this IEndpointRouteBuilder routes)
        {
            var api = routes.MapGroup("/offers")
                .WithTags(["Offers"])
                .RequireAuthorization();

            api.MapPost("/", async (IMediator mediator, CreateOfferCommand command) => await mediator.SendAndMatchAsync(command));
            api.MapGet("/", async (IMediator mediator) => await mediator.Send(new UseCases.Offers.List.ListOffersQuery()));
            api.MapGet("/{id}", async (IMediator mediator, Guid id, bool withItems = false) => await mediator.Send(new UseCases.Offers.Get.GetOfferQuery(new(id))));
            api.MapPut("/{id}", async (IMediator mediator, Guid id, UpdateOfferCommand command) => await mediator.SendAndMatchAsync(command));
            api.MapDelete("/{id}", async (IMediator mediator, Guid id) => await mediator.SendAndMatchAsync(new DeleteOfferCommand(new(id))));

            api.MapPost("/{id}/items", async (IMediator mediator, Guid id, AddOfferItemCommand command) => await mediator.SendAndMatchAsync(command));
            api.MapPut("/{id}/items/{itemId}", async (IMediator mediator, Guid id, Guid itemId, UpdateOfferItemCommand command) => await mediator.SendAndMatchAsync(command));
            api.MapDelete("/{id}/items/{itemId}", async (IMediator mediator, Guid id, Guid itemId) => await mediator.SendAndMatchAsync(new RemoveOfferItemCommand(new(id), new(itemId))));
        }
    }
}
