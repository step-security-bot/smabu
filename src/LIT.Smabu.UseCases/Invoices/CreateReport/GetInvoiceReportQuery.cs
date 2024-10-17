using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.Domain.Shared;
using LIT.Smabu.UseCases.SeedWork;

namespace LIT.Smabu.UseCases.Invoices.CreateReport
{
    public record GetInvoiceReportQuery(InvoiceId Id) : IQuery<IReport>
    {

    }
}