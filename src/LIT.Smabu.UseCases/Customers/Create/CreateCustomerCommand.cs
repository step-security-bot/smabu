﻿using LIT.Smabu.Domain.CustomerAggregate;
using LIT.Smabu.UseCases.SeedWork;

namespace LIT.Smabu.UseCases.Customers.Create
{
    public record CreateCustomerCommand : ICommand<CustomerId>
    {
        public required CustomerId Id { get; set; }
        public required string Name { get; set; }
        public CustomerNumber? Number { get; set; }
    }
}
