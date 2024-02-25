using LIT.Smabu.Domain.CustomerAggregate;
using LIT.Smabu.Domain.InvoiceAggregate;

namespace LIT.Smabu.UseCases.Invoices
{
    public record InvoiceWithItemsDTO : InvoiceDTO
    {
        public InvoiceWithItemsDTO(InvoiceDTO original, List<InvoiceItemDTO> items) : base(original)
        {
            Items = items;
        }

        public List<InvoiceItemDTO> Items { get; set; }
        new public static InvoiceWithItemsDTO From(Invoice invoice, Customer customer)
        {
            return new(InvoiceDTO.From(invoice, customer), invoice.Items.Select(InvoiceItemDTO.From).ToList());
        }
    }
}