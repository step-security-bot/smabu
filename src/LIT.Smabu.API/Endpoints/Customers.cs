using LIT.Smabu.UseCases.Customers.Create;
using LIT.Smabu.UseCases.Customers.Delete;
using LIT.Smabu.UseCases.Customers.Update;
using MediatR;
using LIT.Smabu.UseCases.Customers.List;
using LIT.Smabu.UseCases.Customers.Get;
using LIT.Smabu.UseCases.Customers;
using LIT.Smabu.Domain.CustomerAggregate;
using LIT.Smabu.Shared;

namespace LIT.Smabu.API.Endpoints
{
    public static class Customers
    {
        public static void RegisterCustomersEndpoints(this IEndpointRouteBuilder routes)
        {
            var api = routes.MapGroup("/customers")
                .WithTags(["Customers"])
                .RequireAuthorization();

            api.MapPost("/", async (IMediator mediator, CreateCustomerCommand command) =>
                await mediator.SendAndMatchAsync(command,
                    onSuccess: Results.Ok,
                    onFailure: Results.BadRequest))
                .Produces<CustomerId>();

            api.MapGet("/", async (IMediator mediator) =>
                await mediator.SendAndMatchAsync(new ListCustomersQuery(),
                    onSuccess: Results.Ok,
                    onFailure: Results.BadRequest))
                .Produces<CustomerDTO[]>();

            api.MapGet("/{id}", async (IMediator mediator, Guid id) => 
                await mediator.SendAndMatchAsync(new GetCustomerQuery(new(id)),
                    onSuccess: Results.Ok,
                    onFailure: Results.BadRequest))
                .Produces<CustomerDTO>();

            api.MapPut("/{id}", async (IMediator mediator, Guid id, UpdateCustomerCommand command) => 
                await mediator.SendAndMatchAsync(command,
                    onSuccess: Results.Ok,
                    onFailure: Results.BadRequest))
                .Produces<CustomerId>();

            api.MapDelete("/{id}", async (IMediator mediator, Guid id) =>
                await mediator.SendAndMatchAsync(new DeleteCustomerCommand(new(id)),
                    onSuccess: () => Results.Ok(),
                    onFailure: Results.BadRequest))
                .Produces(200)
                .Produces<Error>(400);
        }
    }
}
