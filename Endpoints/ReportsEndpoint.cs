using Project.Services.Reports;
using Project.Services.ShoppingCartService;

namespace Project.Endpoints
{
    public static class ReportsEndpoint
    {
        public static void RegisterReportEndpoints(this WebApplication app)
        {
            app.MapGet("/Reporting/TotalSales/", async (HttpContext context, IReportingService salesReport) =>
            {
                var totalSales = salesReport.GetTotalSales();
                return Results.Ok($"Total sales since the start of the business is {totalSales}");

            }).RequireAuthorization("adminAccess");

            app.MapGet("/Reporting/TotalSales/Month/{Month}", async (HttpContext context, int month, IReportingService salesReport) =>
            {
                var monthlySales = salesReport.GetMonthlySales(month);
                return Results.Ok($"Total sales in selected month is {monthlySales}");

            }).RequireAuthorization("adminAccess");

            app.MapGet("/Reporting/TotalSales/Year/{Year}", async (HttpContext context, int year, IReportingService salesReport) =>
            {
                var yearlySales = salesReport.GetYearlySales(year);
                return Results.Ok($"Total sales in selected year is {yearlySales}");
            }).RequireAuthorization("adminAccess");

            app.MapGet("/Reporting/MostPopularProducts", async (HttpContext context, IReportingService salesReport) =>
            {
                var mostPopularProducts = salesReport.GetMostPopularProducts();
                return Results.Ok(mostPopularProducts);
            }).RequireAuthorization("adminAccess");

            app.MapGet("/Reporting/TopCustomers", async (HttpContext context, IReportingService salesReport) =>
            {
                var topCustomersList = salesReport.GetTopCustomers();
                return Results.Ok(topCustomersList);

            }).RequireAuthorization("adminAccess");
        }
    }

}
