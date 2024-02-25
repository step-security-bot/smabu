using LIT.Smabu.UseCases.Invoices;

namespace LIT.Smabu.Web.Pages.Shared.Documents
{
    public static class Utils
    {
        public static string CreateFileNamePDF(InvoiceDTO invoice) => $"{invoice.Number.Long}_{invoice.Customer.ShortName}.pdf";
    }
}
