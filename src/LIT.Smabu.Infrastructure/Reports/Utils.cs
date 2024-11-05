using LIT.Smabu.UseCases.Invoices;
using LIT.Smabu.UseCases.Offers;

namespace LIT.Smabu.Infrastructure.Reports
{
    public static class Utils
    {
        public static string CreateFileNamePDF(InvoiceDTO invoice) => $"{invoice.Number.DisplayName}_{invoice.Customer.CorporateDesign.ShortName}.pdf";
        public static string CreateFileNamePDF(OfferDTO offer) => $"{offer.Number.DisplayName}_{offer.Customer.CorporateDesign.ShortName}.pdf";
    }
}
