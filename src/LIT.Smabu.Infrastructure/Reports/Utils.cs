using LIT.Smabu.UseCases.Invoices;
using LIT.Smabu.UseCases.Offers;

namespace LIT.Smabu.Infrastructure.Reports
{
    public static class Utils
    {
        public static string CreateFileNamePDF(InvoiceDTO invoice) => $"{invoice.Number.Long}_{invoice.Customer.CorporateDesign.ShortName}.pdf";
        public static string CreateFileNamePDF(OfferDTO offer) => $"{offer.Number.Long}_{offer.Customer.CorporateDesign.ShortName}.pdf";
    }
}
