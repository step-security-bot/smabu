﻿using LIT.Smabu.Domain.CatalogAggregate;
using LIT.Smabu.Domain.Common;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Catalogs.UpdateItem
{
    public record UpdateCatalogItemCommand(CatalogItemId CatalogItemId, CatalogId CatalogId, string Name, string Description,
        bool IsActive, Unit Unit, CatalogItemPrice[] Prices, CustomerCatalogItemPrice[] CustomerPrices) : ICommand
    {

    }
}