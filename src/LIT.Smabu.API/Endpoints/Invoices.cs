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

            api.MapPost("/", async (IMediator mediator, CreateInvoiceCommand command) => await mediator.SendAndMatchAsync(command));
            api.MapGet("/", async (IMediator mediator) => await mediator.SendAndMatchAsync(new ListInvoicesQuery()));
            api.MapGet("/{id}", async (IMediator mediator, Guid id, bool withItems = false) => await mediator.SendAndMatchAsync(new GetInvoiceQuery(new(id)) { WithItems = withItems }));
            api.MapPut("/{id}", async (IMediator mediator, Guid id, UpdateInvoiceCommand command) => await mediator.SendAndMatchAsync(command));
            api.MapPut("/{id}/release", async (IMediator mediator, Guid id, ReleaseInvoiceCommand command) => await mediator.SendAndMatchAsync(command));
            api.MapPut("/{id}/withdrawrelease", async (IMediator mediator, Guid id, WithdrawReleaseInvoiceCommand command) => await mediator.SendAndMatchAsync(command));
            api.MapDelete("/{id}", async (IMediator mediator, Guid id) => await mediator.SendAndMatchAsync(new DeleteInvoiceCommand(new(id))));

            api.MapPost("/{id}/items", async (IMediator mediator, Guid id, AddInvoiceItemCommand command) => await mediator.SendAndMatchAsync(command));
            api.MapPut("/{id}/items/{itemId}", async (IMediator mediator, Guid id, Guid itemId, UpdateInvoiceItemCommand command) => await mediator.SendAndMatchAsync(command));
            api.MapPut("/{id}/items/{itemId}/movedown", async (IMediator mediator, Guid id, Guid itemId) => await mediator.SendAndMatchAsync(new MoveInvoiceItemDownCommand(new(itemId), new(id))));
            api.MapPut("/{id}/items/{itemId}/moveup", async (IMediator mediator, Guid id, Guid itemId) => await mediator.SendAndMatchAsync(new MoveInvoiceItemUpCommand(new(itemId), new(id))));
            api.MapDelete("/{id}/items/{itemId}", async (IMediator mediator, Guid id, Guid itemId) => await mediator.SendAndMatchAsync(new RemoveInvoiceItemCommand(new(itemId), new(id))));
        }
    }
}