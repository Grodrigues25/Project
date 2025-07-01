using Project.Models.Reports;

namespace Project.Services.Reports
{
    public interface IReportingService
    {
        float GetMonthlySales(int month);
        float GetYearlySales(int year);
        float GetTotalSales();
        List<ReportTopProductsModel> GetMostPopularProducts();
        List<ReportTopCustomersModel> GetTopCustomers();

    }
}
