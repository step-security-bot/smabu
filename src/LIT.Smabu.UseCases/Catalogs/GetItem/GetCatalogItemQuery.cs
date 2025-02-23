﻿using LIT.Smabu.Domain.CatalogAggregate;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Catalogs.GetItem
{
    public record GetCatalogItemQuery(CatalogItemId CatalogItemId, CatalogId CatalogId) : IQuery<CatalogItemDTO>
    {

    }
}