using MediatR;
using LIT.Smabu.UseCases.Orders.Create;
using LIT.Smabu.Domain.OrderAggregate;
using LIT.Smabu.UseCases.Orders;
using LIT.Smabu.UseCases.Orders.List;
using LIT.Smabu.UseCases.Orders.Get;
using LIT.Smabu.UseCases.Orders.Update;
using LIT.Smabu.Domain.Shared;
using LIT.Smabu.UseCases.Orders.Delete;
using LIT.Smabu.Domain.OrderAggregate.Services;
using LIT.Smabu.UseCases.Orders.AddReferences;

namespace LIT.Smabu.API.Endpoints
{
    public static class Orders
    {
        public static void RegisterOrdersEndpoints(this IEndpointRouteBuilder routes)
        {
            var api = routes.MapGroup("/orders")
                .WithTags(["Orders"])
                .RequireAuthorization();

            RegisterOrder(api);
        }

        private static void RegisterOrder(RouteGroupBuilder api)
        {
            api.MapPost("/", async (IMediator mediator, CreateOrderCommand command) =>
                await mediator.SendAndMatchAsync(command,
                    onSuccess: Results.Ok,
                    onFailure: Results.BadRequest))
                .Produces<OrderId>();

            api.MapGet("/", async (IMediator mediator) =>
                await mediator.SendAndMatchAsync(new ListOrdersQuery(),
                    onSuccess: Results.Ok,
                    onFailure: Results.BadRequest))
                .Produces<OrderDTO[]>();

            api.MapGet("/{id}", async (IMediator mediator, Guid id, bool withItems = false) =>
                await mediator.SendAndMatchAsync(new GetOrderQuery(new(id)),
                    onSuccess: Results.Ok,
                    onFailure: Results.BadRequest))
                .Produces<OrderDTO[]>();

            api.MapPut("/{id}", async (IMediator mediator, Guid id, UpdateOrderCommand command) =>
                await mediator.SendAndMatchAsync(command,
                    onSuccess: Results.Ok,
                    onFailure: Results.BadRequest))
                .Produces<OrderId>()
                .Produces<Error>(400);

            api.MapPut("/{id}/updateReferences", async (IMediator mediator, Guid id, UpdateReferencesToOrderCommand command) =>
                await mediator.SendAndMatchAsync(command,
                    onSuccess: () => Results.Ok(),
                    onFailure: Results.BadRequest))
                .Produces(200)
                .Produces<Error>(400);

            api.MapDelete("/{id}", async (IMediator mediator, Guid id) =>
                await mediator.SendAndMatchAsync(new DeleteOrderCommand(new(id)),
                    onSuccess: () => Results.Ok(),
                    onFailure: Results.BadRequest))
                .Produces(200)
                .Produces<Error>(400);
        }
    }
}
