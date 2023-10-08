using LIT.Smabu.Shared.Contracts;

namespace LIT.Smabu.Shared.Dashboards
{
    public class HomeDashboardDTO : IDTO
    {
        public string Sales3Month { get; set; }
        public string Sales6Month { get; set; }
        public string Sales12Month { get; set; }
        public string BestSalesCustomer1Name { get; set; }
        public string BestSalesCustomer1Value { get; set; }
        public string BestSalesCustomer2Name { get; set; }
        public string BestSalesCustomer2Value { get; set; }
        public string BestSalesCustomer3Name { get; set; }
        public string BestSalesCustomer3Value { get; set; }
        public string TotalSales { get; set; }
        public string SalesCurrentYear { get; set; }
        public string SalesLastYear { get; set; }
    }
}
