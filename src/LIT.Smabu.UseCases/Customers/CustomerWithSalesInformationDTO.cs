namespace LIT.Smabu.UseCases.Customers
{
    public record CustomerWithSalesInformationDTO : CustomerDTO
    {
        protected CustomerWithSalesInformationDTO(CustomerDTO original) : base(original)
        {
        }

        public decimal TotalSales { get; set; }
    }
}