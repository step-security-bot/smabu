﻿using LIT.Smabu.Domain.CatalogAggregate;
using LIT.Smabu.Domain.Common;
using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Invoices.AddInvoiceItem
{
    public record AddInvoiceItemCommand : ICommand<InvoiceItemId>
    {
        public required InvoiceItemId InvoiceItemId { get; set; }
        public required InvoiceId InvoiceId { get; set; }
        public required string Details { get; set; }
        public required Quantity Quantity { get; set; }
        public required decimal UnitPrice { get; set; }
        public CatalogItemId? CatalogItemId { get; set; }
    }
}
