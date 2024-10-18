using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.Shared;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Invoices.CreateReport
{
    public record GetInvoiceReportQuery(InvoiceId Id) : IQuery<IReport>
    {

    }
}