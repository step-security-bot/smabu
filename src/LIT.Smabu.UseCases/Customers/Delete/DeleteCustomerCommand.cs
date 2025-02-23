﻿using LIT.Smabu.Domain.CustomerAggregate;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Customers.Delete
{
    public record DeleteCustomerCommand(CustomerId CustomerId) : ICommand
    {

    }
}
