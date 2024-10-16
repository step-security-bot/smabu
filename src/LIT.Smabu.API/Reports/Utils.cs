using LIT.Smabu.UseCases.Invoices;
using LIT.Smabu.UseCases.Offers;

namespace LIT.Smabu.API.Reports
{
    public static class Utils
    {
        public static string CreateFileNamePDF(InvoiceDTO invoice) => $"{invoice.Number.Long}_{invoice.Customer.ShortName}.pdf";
        public static string CreateFileNamePDF(OfferDTO offer) => $"{offer.Number.Long}_{offer.Customer.ShortName}.pdf";
    }
}
