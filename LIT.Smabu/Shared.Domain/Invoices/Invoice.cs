﻿using LIT.Smabu.Domain.Shared.Common;
using LIT.Smabu.Domain.Shared.Contracts;
using LIT.Smabu.Domain.Shared.Customers;
using LIT.Smabu.Domain.Shared.Offers;
using LIT.Smabu.Domain.Shared.Orders;
using LIT.Smabu.Domain.Shared.Products;

namespace LIT.Smabu.Domain.Shared.Invoices
{
    public class Invoice : AggregateRoot<InvoiceId>
    {
        public Invoice(InvoiceId id, CustomerId customerId, InvoiceNumber number, DatePeriod performancePeriod, Currency currency, decimal tax, string taxDetails,
            OrderId? orderId, OfferId? offerId, List<InvoiceLine> invoiceLines)
        {
            Id = id;
            CustomerId = customerId;
            Number = number;
            PerformancePeriod = performancePeriod;
            Currency = currency;
            OrderId = orderId;
            OfferId = offerId;
            Tax = tax;
            TaxDetails = taxDetails;
            InvoiceLines = invoiceLines;
        }

        public override InvoiceId Id { get; }
        public CustomerId CustomerId { get; }
        public InvoiceNumber Number { get; }
        public DatePeriod PerformancePeriod { get; private set; }
        public DateOnly? InvoiceDate { get; private set; }
        public int FiscalYear => PerformancePeriod.To.Year;
        public decimal Tax { get; private set; }
        public string TaxDetails { get; private set; }
        public OrderId? OrderId { get; private set; }
        public OfferId? OfferId { get; private set; }
        public List<InvoiceLine> InvoiceLines { get; }
        public decimal Amount => InvoiceLines.Sum(x => x.TotalPrice);
        public Currency Currency { get; }
        public DateOnly SalesReportDate => this.DetermineSalesReportDate();

        public static Invoice Create(InvoiceId id, CustomerId customerId, InvoiceNumber number, DatePeriod performancePeriod, Currency currency, decimal tax, string taxDetails,
            OrderId? orderId = null, OfferId? offerId = null)
        {
            return new Invoice(id, customerId, number, performancePeriod, currency, tax, taxDetails, orderId, offerId, new List<InvoiceLine>());
        }

        public void Edit(DatePeriod performancePeriod, decimal tax, string taxDetails)
        {
            this.PerformancePeriod = performancePeriod;
            this.Tax = tax; 
            this.TaxDetails = taxDetails;
        }

        public InvoiceLine AddInvoiceLine(string details, Quantity quantity, decimal unitPrice, ProductId? productId = null)
        {
            var id = new InvoiceLineId(Guid.NewGuid());
            var position = InvoiceLines.OrderByDescending(x => x.Position).FirstOrDefault()?.Position + 1 ?? 1;
            var result = new InvoiceLine(id, Id, position, details, quantity, unitPrice, productId);
            InvoiceLines.Add(result);
            return result;
        }

        public InvoiceLine EditInvoiceLine(InvoiceLineId id, string details, Quantity quantity, decimal unitPrice)
        {
            var invoiceLine = this.InvoiceLines.Find(x => x.Id == id)!;
            invoiceLine.Edit(details, quantity, unitPrice);
            return invoiceLine;
        }

        private DateOnly DetermineSalesReportDate()
        {
            if (this.InvoiceDate != null)
            {
                return this.InvoiceDate.Value;
            }
            else if (this.PerformancePeriod?.To != null)
            {
                return this.PerformancePeriod.To;
            }
            else if (this.PerformancePeriod?.From != null)
            {
                return this.PerformancePeriod.From;
            }
            else if (this.Meta != null)
            {
                return DateOnly.FromDateTime(this.Meta!.CreatedOn);
            }
            else
            {
                return DateOnly.FromDateTime(DateTime.Now);
            }
        }
    }
}

