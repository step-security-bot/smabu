using LIT.Smabu.Domain.CustomerAggregate;
using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.Domain.SeedWork;
using LIT.Smabu.UseCases.Invoices;
using LIT.Smabu.UseCases.Invoices.AddInvoiceItem;
using LIT.Smabu.UseCases.Invoices.Create;
using LIT.Smabu.UseCases.Invoices.Delete;
using LIT.Smabu.UseCases.Invoices.Get;
using LIT.Smabu.UseCases.Invoices.List;
using LIT.Smabu.UseCases.Invoices.MoveInvoiceItem;
using LIT.Smabu.UseCases.Invoices.Release;
using LIT.Smabu.UseCases.Invoices.RemoveInvoiceItem;
using LIT.Smabu.UseCases.Invoices.Update;
using LIT.Smabu.UseCases.Invoices.UpdateInvoiceItem;
using LIT.Smabu.UseCases.Invoices.WithdrawRelease;
using MediatR;

namespace LIT.Smabu.API.Endpoints
{
    public static class Invoices
    {
        public static void RegisterInvoicesEndpoints(this IEndpointRouteBuilder routes)
        {
            var api = routes.MapGroup("/invoices")
                .WithTags(["Invoices"])
                .RequireAuthorization();

            RegisterInvoice(api);
            RegisterInvoiceItem(api);
        }

        private static void RegisterInvoice(RouteGroupBuilder api)
        {
            api.MapPost("/", async (IMediator mediator, CreateInvoiceCommand command) =>
                await mediator.SendAndMatchAsync(command,
                    onSuccess: Results.Ok,
                    onFailure: Results.BadRequest))
                .Produces<InvoiceId>();

            api.MapGet("/", async (IMediator mediator) =>
                await mediator.SendAndMatchAsync(new ListInvoicesQuery(),
                    onSuccess: Results.Ok,
                    onFailure: Results.BadRequest))
                .Produces<InvoiceDTO[]>();

            api.MapGet("/{id}", async (IMediator mediator, Guid id, bool withItems = false) =>
                await mediator.SendAndMatchAsync(new GetInvoiceQuery(new(id)) { WithItems = withItems },
                    onSuccess: Results.Ok,
                    onFailure: Results.BadRequest))
                .Produces<InvoiceDTO[]>();

            api.MapPut("/{id}", async (IMediator mediator, Guid id, UpdateInvoiceCommand command) =>
                await mediator.SendAndMatchAsync(command,
                    onSuccess: Results.Ok,
                    onFailure: Results.BadRequest))
                .Produces<InvoiceId>()
                .Produces<Error>(400);

            api.MapPut("/{id}/release", async (IMediator mediator, Guid id, ReleaseInvoiceCommand command) =>
                await mediator.SendAndMatchAsync(command,
                    onSuccess: () => Results.Ok(),
                    onFailure: Results.BadRequest))
                .Produces(200)
                .Produces<Error>(400);

            api.MapPut("/{id}/withdrawrelease", async (IMediator mediator, Guid id, WithdrawReleaseInvoiceCommand command) =>
                await mediator.SendAndMatchAsync(command,
                    onSuccess: () => Results.Ok(),
                    onFailure: Results.BadRequest))
                .Produces(200)
                .Produces<Error>(400);

            api.MapDelete("/{id}", async (IMediator mediator, Guid id) =>
                await mediator.SendAndMatchAsync(new DeleteInvoiceCommand(new(id)),
                    onSuccess: () => Results.Ok(),
                    onFailure: Results.BadRequest))
                .Produces(200)
                .Produces<Error>(400);
        }

        private static void RegisterInvoiceItem(RouteGroupBuilder api)
        {
            api.MapPost("/{id}/items", async (IMediator mediator, Guid id, AddInvoiceItemCommand command) =>
                await mediator.SendAndMatchAsync(command,
                    onSuccess: Results.Ok,
                    onFailure: Results.BadRequest))
                .Produces<InvoiceItemId>();

            api.MapPut("/{id}/items/{itemId}", async (IMediator mediator, Guid id, Guid itemId, UpdateInvoiceItemCommand command) =>
                await mediator.SendAndMatchAsync(command,
                    onSuccess: Results.Ok,
                    onFailure: Results.BadRequest))
                .Produces<InvoiceItemId>();

            api.MapPut("/{id}/items/{itemId}/movedown", async (IMediator mediator, Guid id, Guid itemId) =>
                await mediator.SendAndMatchAsync(new MoveInvoiceItemDownCommand(new(itemId), new(id)),
                    onSuccess: () => Results.Ok(),
                    onFailure: Results.BadRequest))
                .Produces(200)
                .Produces<Error>(400);

            api.MapPut("/{id}/items/{itemId}/moveup", async (IMediator mediator, Guid id, Guid itemId) =>
                await mediator.SendAndMatchAsync(new MoveInvoiceItemUpCommand(new(itemId), new(id)),
                    onSuccess: () => Results.Ok(),
                    onFailure: Results.BadRequest))
                .Produces(200)
                .Produces<Error>(400);

            api.MapDelete("/{id}/items/{itemId}", async (IMediator mediator, Guid id, Guid itemId) =>
                await mediator.SendAndMatchAsync(new RemoveInvoiceItemCommand(new(itemId), new(id)),
                    onSuccess: () => Results.Ok(),
                    onFailure: Results.BadRequest))
                .Produces(200)
                .Produces<Error>(400);
        }
    }
}